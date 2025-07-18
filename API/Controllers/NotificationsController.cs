using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRPM.API.Models;
using SRPM.API.Services;
using System.Security.Claims;
using Task = System.Threading.Tasks.Task;
using Swashbuckle.AspNetCore.Annotations;

namespace SRPM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy thông báo theo ID")]
        public async Task<ActionResult<NotificationDto>> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null || notification.UserId != userId)
                return NotFound();

            return Ok(notification);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetByUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var notifications = await _notificationService.GetByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpGet("unread")]
        [SwaggerOperation(Summary = "Lấy thông báo chưa đọc")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUnreadByUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var notifications = await _notificationService.GetUnreadByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpPatch("{id}/mark-as-read")]
        [SwaggerOperation(Summary = "Đánh dấu thông báo là đã đọc")]
        public async Task<ActionResult> MarkAsRead(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _notificationService.MarkAsReadAsync(id, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpPatch("mark-all-as-read")]
        [SwaggerOperation(Summary = "Đánh dấu tất cả thông báo là đã đọc")]
        public async Task<ActionResult> MarkAllAsRead()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _notificationService.MarkAllAsReadAsync(userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xóa thông báo")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _notificationService.DeleteAsync(id, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }
    }
}
