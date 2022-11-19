using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Roqeeb_Project.Auth.Service;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserStore<User> _userRepository;
        private readonly IUserRoleStore<User> _userRoleRepository;
        public CustomerService(ICustomerRepository customerRepository, IIdentityService identityService, IUserStore<User> userRepository, IUserRoleStore<User> userRoleRepository)
        {
            _customerRepository = customerRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<BaseResponse<CustomerDTO>> CreateCustomer(RegisterCustomerRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var getUsername = await _customerRepository.GetCustomerByUserNameAsync(request.Username, cancellationToken);
            if (getUsername != null) return new BaseResponse<CustomerDTO>
            {
                Message = "Username already exists",
                Status = false
            };
            var getCustomer = await _customerRepository.GetCustomerAsync(c => c.Email == request.Email, cancellationToken);
            if (getCustomer != null) return new BaseResponse<CustomerDTO>
            {
                Message = "Customer already exists",
                Status = false
            };
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Salt = _identityService.GenerateSalt(),
                Password = ""
            };
            await _userRepository.CreateAsync(user, cancellationToken);
            var newUser = await _userRepository.FindByIdAsync(user.Id, cancellationToken);
            newUser.Password = $"{request.Password}{newUser.Salt}";
            await _userRepository.UpdateAsync(newUser, cancellationToken);
            await _userRoleRepository.AddToRoleAsync(user, "Customer", cancellationToken);
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Age = request.Age,
                UserId = user.Id
            };
            await _customerRepository.CreateCustomerAsync(customer, cancellationToken);
            return new BaseResponse<CustomerDTO>
            {
                Data = new CustomerDTO
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Orders = null
                },
                Message = "Registration Successfull",
                Status = true
            };
        }
        public async Task<BaseResponse<IEnumerable<CustomerDTO>>> ViewAllCustomers(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var customers = await _customerRepository.GetAll(cancellationToken);
            if (customers.Count == 0) return new BaseResponse<IEnumerable<CustomerDTO>>
            {
                Message = "No Customers",
                Status = false
            };
            return new BaseResponse<IEnumerable<CustomerDTO>>
            {
                Data = customers.Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Username = c.User.Username,
                }).ToList(),
                Message = "Successfull",
                Status = true
            };


        }
    }
}
