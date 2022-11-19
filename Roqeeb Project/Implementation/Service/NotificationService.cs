using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Implementation.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<BaseResponse<NotificationDTO>> ClearAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notifications = await _notificationRepository.GetAll(cancellationToken);
            foreach(var notification in notifications)
            {
                await _notificationRepository.DeleteAsync(notification, cancellationToken);
            }

            return new BaseResponse<NotificationDTO>
            {
                Message = "Deletd Successfully",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<NotificationDTO>>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notifications = await _notificationRepository.GetAll(cancellationToken);
            if (notifications.Count == 0) return new BaseResponse<IList<NotificationDTO>>
            {
                Message = "No Messages",
                Status = false
            };
            return new BaseResponse<IList<NotificationDTO>>
            {
                Data = notifications.Select(n => new NotificationDTO
                {
                    Id = n.Id,
                    Message = n.Message,
                }).ToList(),
                Message = "Successful",
                Status = true
            };

        }

        public async Task<BaseResponse<IList<NotificationDTO>>> GetAllReadMessages(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notifications = await _notificationRepository.GetAllAsync(n => n.IsRead == true, cancellationToken);
            if (notifications.Count == 0) return new BaseResponse<IList<NotificationDTO>>
            {
                Message = "No Messages",
                Status = false
            };
            return new BaseResponse<IList<NotificationDTO>>
            {
                Data = notifications.Select(n => new NotificationDTO
                {
                    Id = n.Id,
                    Message = n.Message,
                }).ToList(),
                Message = "Successful",
                Status = true
            };
        }

        public async Task<BaseResponse<NotificationDTO>> ReadMessage(string notificationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notification = await _notificationRepository.GetAsync(n => n.Id == notificationId, cancellationToken);
            if (notification == null) return new BaseResponse<NotificationDTO>
            {
                Message = "Not Found",
                Status = false
            };
            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification, cancellationToken);
            return new BaseResponse<NotificationDTO>
            {
               
                Message = "Succesfull",
                Status = true
            };

        }
    }
}
