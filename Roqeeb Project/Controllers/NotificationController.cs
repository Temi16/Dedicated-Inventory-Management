using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roqeeb_Project.Interface.Service;

namespace Roqeeb_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("UnreadNotifications")]
        public async  Task<IActionResult> AllUnreadNotifications(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notifications = await _notificationService.GetAll(cancellationToken);
            if (notifications.Status == false) return BadRequest(notifications);
            return Ok(notifications);
        }
    }
}
