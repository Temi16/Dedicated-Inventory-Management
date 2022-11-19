using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUserStore<User> _userRepository;
        private readonly IOrderService _orderService;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IUserStore<User> userRepository, IOrderService orderService, IPaymentRepository paymentRepository)
        {
            _userRepository = userRepository;
            _orderService = orderService;
            _paymentRepository = paymentRepository;
        }
        public Task<PaymentsResponseModel> GetAllPaymentsByCustomer(string customerId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PaystackResponse> MakePayment(PaymentRequestModel model, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(model.UserId, cancellationToken);
            if (user == null)
            {
                return new PaystackResponse
                {
                    status = false,
                    message = "you are not a customer"
                };
            }
            var pay = model.AmountPaid;
            var generateRef = Guid.NewGuid().ToString().Substring(0, 10);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "https://api.paystack.co/transaction/initialize";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_test_c43bd7866c1bd0dd38c7bfcea1c03290ae02d5d3");
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                amount = model.AmountPaid,
                referrenceNumber = generateRef

            }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var resString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonSerializer.Deserialize<PaystackResponse>(resString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return new PaystackResponse
                //{
                //    status = true,
                //    message = "payment successfull"
                //};

                var sendMoneyDto = new SendMoneyDTO
                {
                    AccountNumber = "2209363463",
                    BankCode = "*966#",
                    Amount = pay
                };
                var sendLandlordMoney = await VerifyAccountNumber(sendMoneyDto.AccountNumber, sendMoneyDto.BankCode, sendMoneyDto.Amount);
                return new PaystackResponse
                {
                    status = true,
                    message = responseObj.message,
                    data = new PaystackData
                    {
                        authorization_url = responseObj.data.authorization_url,
                        reference = generateRef
                    }
                };
               
            }
            var order = _orderService.Create(model.CartId, user.Username, cancellationToken);
            var payment = new Payment
            {
                UserId = user.Id,
                OrderId = order.Id.ToString(),
                AmountPaid = model.AmountPaid,
                DateOfPayment = DateTime.UtcNow.ToString(),
                ReferrenceNumber = generateRef

            };
            var createPayment = await _paymentRepository.CreateAsync(payment, cancellationToken);
            if (createPayment == null)
            {
                return new PaystackResponse
                {
                    message = "Payment not successfull try again",
                    status = false,
                };
            }
            return new PaystackResponse
            {
                message = responseObj.message,
                status = true
            };
        }
    

        public Task<BaseResponse<PaymentDTO>> VerifyPayment(string referrenceNumber, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        public async Task<VerifyBank> VerifyAccountNumber(string acNumber, string bankCode, decimal amount)
        {
            var getHttpClient = new HttpClient();
            getHttpClient.DefaultRequestHeaders.Accept.Clear();
            getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var baseUri = getHttpClient.BaseAddress =
                new Uri($"https://api.paystack.co/bank/resolve?account_number={acNumber}&bank_code={bankCode}");

            getHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "sk_test_6483775b59a2152f947af8583a987e98eb5c7af2");
            var response = await getHttpClient.GetAsync(baseUri);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<VerifyBank>(responseString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!responseObject.status)
                {
                    return new VerifyBank()
                    {
                        status = false,
                        message = responseObject.message
                    };
                }

                var splitName = responseObject.data.account_name;
                var splitNameArray = splitName.Split(' ');
                var generate = await GenerateRecipients(responseObject);
                if (!generate.status)
                {
                    return new VerifyBank()
                    {
                        status = false,
                        message = generate.message
                    };
                }

                var makeTransfer = await SendMoney(generate.data.recipient_code, amount);
                if (!makeTransfer.status)
                {
                    return new VerifyBank()
                    {
                        status = false,
                        message = makeTransfer.message
                    };
                }
                return new VerifyBank()
                {
                    status = true,
                    message = makeTransfer.message,
                    data = new VerifyBankData()
                    {
                        reason = generate.data.reason,
                        reference = generate.data.reference,
                        recipient_code = generate.data.recipient_code,
                        amount = makeTransfer.data.amount,
                        currency = makeTransfer.data.currency,
                        status = makeTransfer.data.status,
                        transfer_code = makeTransfer.data.transfer_code
                    }
                };
            }

            return new VerifyBank()
            {
                status = false,
                message = "Cannot verify account number"
            };
        }
        public async Task<GenerateRecipient> GenerateRecipients(VerifyBank verifyBank)
        {

            var getHttpClient = new HttpClient();
            getHttpClient.DefaultRequestHeaders.Accept.Clear();
            getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var baseUri = getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/transferrecipient");
            getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_test_c43bd7866c1bd0dd38c7bfcea1c03290ae02d5d3");
            var response = await getHttpClient.PostAsJsonAsync(baseUri, new
            {
                type = "nuban",
                name = verifyBank.data.account_name,
                account_number = verifyBank.data.account_number,
                bank_code = verifyBank.data.bank_code,
                currency = "NGN",
            });
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GenerateRecipient>(responseString);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                if (!responseObject.status)
                {
                    return new GenerateRecipient()
                    {
                        status = false,
                        message = responseObject.message
                    };
                }
                return new GenerateRecipient()
                {
                    status = true,
                    message = "Recipient Generated",
                    data = responseObject.data
                };
            }

            return new GenerateRecipient()
            {
                status = false,
                message = responseObject.message
            };
        }
        private async Task<MakeATransfer> SendMoney(string recip, decimal amount)
        {
            var getHttpClient = new HttpClient();
            getHttpClient.DefaultRequestHeaders.Accept.Clear();
            getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var baseUri = $"https://api.paystack.co/transfer";
            getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_test_c43bd7866c1bd0dd38c7bfcea1c03290ae02d5d3");
            var response = await getHttpClient.PostAsJsonAsync(baseUri, new
            {

                recipient = recip,
                amount = amount * 100,
                currency = "NGN",
                source = "balance"
            });
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<MakeATransfer>(responseString);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!responseObject.status)
                {
                    return new MakeATransfer()
                    {
                        status = false,
                        message = responseObject.message
                    };
                }
                return new MakeATransfer()
                {
                    status = true,
                    message = responseObject.message,
                    data = responseObject.data
                };
            }
            return new MakeATransfer()
            {
                status = false,
                message = "Pls retry payment is not successfull"
            };
        }
    }
}
