using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Roqeeb_Project.Context;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Identity;

namespace Roqeeb_Project.Implementation.Identity.Repositories
{
    public class UserRepository : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>, IUserEmailStore<User>, IQueryableUserStore<User>, IUserPhoneNumberStore<User>

    { 
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IQueryable<User> Users => throw new System.NotImplementedException();

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            roleName = roleName.ToLower();
            if (user == null) throw new ArgumentNullException(null);
            if(string.IsNullOrEmpty(roleName)) throw new ArgumentNullException(nameof(roleName));
            var myUser = await FindByIdAsync(user.Id, cancellationToken);
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Name.ToLower() == roleName, cancellationToken);
            var userRole = new UserRole
            {
                UserId = myUser.Id,
                RoleId = role.Id,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
               
            };
            await _context.UserRoles.AddAsync(userRole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            if(string.IsNullOrEmpty(normalizedEmail))
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }
            normalizedEmail = normalizedEmail.ToLower();
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
            return user;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
            return user;
            
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            normalizedUserName = normalizedUserName.ToLower();
            if(string.IsNullOrEmpty(normalizedUserName))
            {
                throw new ArgumentNullException(nameof(normalizedUserName));
            }
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Username.ToLower() == normalizedUserName, cancellationToken);
            return user;
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(user == null)
            {
                throw new ArgumentNullException();
            }
            var myUser = await FindByIdAsync(user.Id, cancellationToken);
            return myUser.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await Task.FromResult(user.IsEmailConfirmed);
            
            
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var myUser = await FindByEmailAsync(user.Email, cancellationToken);
            if (myUser == null) return "null";
            var salt = myUser.Salt;
            var passwordHash = $"{user.Password}";
            return passwordHash;

        }
       

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(user == null) throw new ArgumentNullException(null);
            var myUser = await FindByIdAsync(user.Id, cancellationToken);
            var roles =  myUser.UserRoles.Where(ur => ur.UserId == user.Id).Select(u => u.Role.Name).ToList();
            return roles;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
