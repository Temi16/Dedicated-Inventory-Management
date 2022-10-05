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
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserStore<User> _userRepository;
        private readonly IIdentityService _identityService;
        public EmployeeService(IEmployeeRepository employeeRepository, IUserStore<User> userRepository, IIdentityService identityService)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _identityService = identityService;
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
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                IsEmailConfirmed = false,
                Salt = _identityService.GenerateSalt()
                
            };
            await _userRepository.CreateAsync(user, cancellationToken);
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
            var user = await _userRepository.FindByIdAsync(employee.UserId, cancellationToken);
            user.Username = request.UserName;
            user.Password = $"{request.Password}{user.Salt}";
            user.IsEmailConfirmed = true;
            await _userRepository.UpdateAsync(user, cancellationToken);
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




    }
}
