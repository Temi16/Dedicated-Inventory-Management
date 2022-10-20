using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification, CancellationToken cancellationToken);
        Task<Notification> UpdateAsync(Notification notification, CancellationToken cancellationToken);
        Task<Notification> DeleteAsync(Notification notification, CancellationToken cancellationToken);
        Task<Notification> GetAsync(Expression<Func<Notification, bool>> expression, CancellationToken cancellationToken);
        Task<IList<Notification>> GetAll(CancellationToken cancellationToken);
        Task<IList<Notification>> GetAllAsync(Expression<Func<Notification, bool>> expression, CancellationToken cancellationToken);
    }
}
