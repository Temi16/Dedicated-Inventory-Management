using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.DTO_s;
using Roqeeb_Project.View_Models.ResponseModels;

namespace Roqeeb_Project.Interface.Service
{
    public interface INotificationService
    {
        Task<BaseResponse<NotificationDTO>> ReadMessage(string notificationId, CancellationToken cancellationToken);
        Task<BaseResponse<NotificationDTO>> ClearAll(CancellationToken cancellationToken);
        Task<BaseResponse<IList<NotificationDTO>>> GetAll(CancellationToken cancellationToken);
        Task<BaseResponse<IList<NotificationDTO>>> GetAllReadMessages(CancellationToken cancellationToken);
    }
}
