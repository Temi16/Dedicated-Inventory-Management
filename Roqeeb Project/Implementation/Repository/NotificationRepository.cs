using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roqeeb_Project.Context;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;

namespace Roqeeb_Project.Implementation.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationContext _context;
        public NotificationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateAsync(Notification notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return notification;
        }
        public async Task<Notification> GetAsync(Expression<Func<Notification, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notification = await _context.Notifications
                .SingleOrDefaultAsync(expression, cancellationToken);
            return notification;
        }

        public async Task<Notification> UpdateAsync(Notification notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return notification;
        }
        public async Task<IList<Notification>> GetAllAsync(Expression<Func<Notification, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notifications = await _context.Notifications
                .Where(expression)
                .ToListAsync(cancellationToken);
            return notifications;
        }

        public async Task<Notification> DeleteAsync(Notification notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return notification;
        }

        public async Task<IList<Notification>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notifications = await _context.Notifications
                .ToListAsync(cancellationToken);
            return notifications;
        }
    }
}
