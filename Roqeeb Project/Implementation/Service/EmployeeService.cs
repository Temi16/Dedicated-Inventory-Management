using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Roqeeb_Project.Auth.Service;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Implementation.Identity.Repositories;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.SendMail;
using Roqeeb_Project.View_Models.ResponseModels;
using static Roqeeb_Project.SendMail.EmailDTO;

namespace Roqeeb_Project.Implementation.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserStore<User> _userRepository;
        private readonly IUserRoleStore<User> _userRoleRepository;
        private readonly IIdentityService _identityService;
        private readonly IMailMessage _email;
        public EmployeeService(IEmployeeRepository employeeRepository, IUserStore<User> userRepository, IIdentityService identityService, IMailMessage email, IUserRoleStore<User> userRoleRepository, IProductRepository productRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _identityService = identityService;
            _email = email;
            _userRoleRepository = userRoleRepository;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<EmployeeDTO>> CreateEmployee(CreateEmployeeRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _employeeRepository.GetEmployeeAsync(e => e.Email == request.Email, cancellationToken);
            if (employee != null) return new BaseResponse<EmployeeDTO>
            {
                Message = "Email Exists",
                Status = false
            };
            var mailRequest = new EmailRequestModel
            {
                ReceiverEmail = request.Email,
                Message = $"Congratulations {request.FirstName}, you now have access to the Golden Inventory app. Click the link to continue",
                ReceiverName = $"{request.FirstName} {request.LastName}",
                Subject = "Employee Registration"
            };
            await _email.SendEmail(mailRequest);
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                IsEmailConfirmed = false,
                Salt = _identityService.GenerateSalt()
                
            };
            await _userRepository.CreateAsync(user, cancellationToken);
            await _userRoleRepository.AddToRoleAsync(user, "Employee", cancellationToken);
            var newEmployee = new Employee
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserId = user.Id,

            };
            await _employeeRepository.CreayeAsync(newEmployee, cancellationToken);
           
            return new BaseResponse<EmployeeDTO>
            {
                Data = new EmployeeDTO
                {
                    Id = newEmployee.Id,
                    Email = newEmployee.Email,
                },
                Message = "Successfully added",
                Status = true
            };
        }
        public async Task<BaseResponse<EmployeeDTO>> ConfirmEmployee(ConfirmEmployeeRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _employeeRepository.GetEmployeeAsync(e => e.Email == request.Email, cancellationToken);
            if (employee == null) return new BaseResponse<EmployeeDTO>
            {
                Message = "Employee does not exist",
                Status = false
            };
            var getUser = await _userRepository.FindByNameAsync(request.UserName, cancellationToken);
            if (getUser != null) return new BaseResponse<EmployeeDTO>
            {
                Message = "This username already exists",
                Status = false
            };
            var user = await _userRepository.FindByIdAsync(employee.UserId, cancellationToken);
            user.Username = request.UserName;
            user.Password = $"{request.Password}{user.Salt}";
            user.IsEmailConfirmed = true;
            await _userRepository.UpdateAsync(user, cancellationToken);
            employee.User.Username = request.UserName;
            employee.Age = request.Age;
            employee.PhoneNumber = request.PhoneNumber;
            await _employeeRepository.UpdateEmployee(employee, cancellationToken);
            return new BaseResponse<EmployeeDTO>
            {

                Data = new EmployeeDTO
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    UserName = employee.User.Username,
                    Age = employee.Age,
                    PhoneNumber = employee.PhoneNumber
                },
                Message = "Successfully updated",
                Status = true
            };


        }
        public async Task<BaseResponse<EmployeeDTO>> UpdateEmployee(UpdateEmployeeRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _employeeRepository.GetEmployeeAsync(e => e.Email == request.Email, cancellationToken);
            if (employee == null) return new BaseResponse<EmployeeDTO>
            {
                Message = "Employee does not exist",
                Status = false
            };
            employee.User.Username = request.UserName;
            employee.Age = request.Age;
            employee.Email = request.Email;
            employee.PhoneNumber = request.PhoneNumber;
            await _employeeRepository.UpdateEmployee(employee, cancellationToken);
            return new BaseResponse<EmployeeDTO>
            {

                Data = new EmployeeDTO
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    UserName = employee.User.Username,
                    Age = employee.Age,
                    PhoneNumber = employee.PhoneNumber
                },
                Message = "Successfully updated",
                Status = true
            };
        }
        public async Task<BaseResponse<IEnumerable<EmployeeDTO>>> ViewAllEmployees(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employees = await _employeeRepository.GetAll(cancellationToken);
            if (employees.Count == 0) return new BaseResponse<IEnumerable<EmployeeDTO>>
            {
                Message = "No Employees",
                Status = false
            };
            return new BaseResponse<IEnumerable<EmployeeDTO>>
            {
                Data = employees.Select(c => new EmployeeDTO
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    UserName = c.User.Username,
                    PhoneNumber = c.PhoneNumber
                }).ToList(),
                Message = "Successfull",
                Status = true
            };


        }

        public async Task<BaseResponse<IEnumerable<TrackDTO>>> ProductDetails(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var products = await _productRepository.ViewProductsAsync(cancellationToken);
            if (products.Count == 0) return new BaseResponse<IEnumerable<TrackDTO>>
            {
                Message = "No products available",
                Status = false
            };
            var storeName = "";
            var newSection = "";
            foreach(var product in products)
            {
                foreach (var section in product.productSections)
                {
                    newSection = section.Section.SectionName;
                    storeName = section.Section.Store.StoreName;
                }
            }
           
            return new BaseResponse<IEnumerable<TrackDTO>>
            {
                Data = products.Select(p => new TrackDTO
                {
                    ProductName = p.ProductName,
                    Image = p.Image,
                    Quantity = p.Quantity,
                    SectionName = newSection,
                    StoreName = storeName
                }).ToList(),
                Message = "Successfull",
                Status = true
            };
        }
    }
}
