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
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy thông tin chi tiết nhiệm vụ theo ID")]
        public async Task<ActionResult<TaskDto>> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy danh sách tất cả nhiệm vụ")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("project/{projectId}")]
        [SwaggerOperation(Summary = "Lấy nhiệm vụ theo ID dự án")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetByProjectId(int projectId)
        {
            var tasks = await _taskService.GetByProjectIdAsync(projectId);
            return Ok(tasks);
        }

        [HttpGet("assigned/{userId}")]
        [SwaggerOperation(Summary = "Lấy nhiệm vụ theo ID người được giao")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetByAssignedToId(int userId)
        {
            var tasks = await _taskService.GetByAssignedToIdAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("status/{status}")]
        [SwaggerOperation(Summary = "Lọc nhiệm vụ theo trạng thái")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetByStatus(string status)
        {
            var tasks = await _taskService.GetByStatusAsync(status);
            return Ok(tasks);
        }

        [HttpGet("milestones/{projectId}")]
        [SwaggerOperation(Summary = "Lấy danh sách các mốc thời gian của dự án")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetMilestones(int projectId)
        {
            var tasks = await _taskService.GetMilestonesAsync(projectId);
            return Ok(tasks);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Tạo mới nhiệm vụ")]
        public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var task = await _taskService.CreateAsync(request, userId);
            if (task == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Cập nhật thông tin nhiệm vụ")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTaskRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _taskService.UpdateAsync(id, request, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xóa nhiệm vụ theo ID")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _taskService.DeleteAsync(id, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [SwaggerOperation(Summary = "Cập nhật trạng thái nhiệm vụ")]
        public async Task<ActionResult> UpdateStatus(int id, [FromBody] UpdateTaskStatusRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _taskService.UpdateStatusAsync(id, request, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }
    }
}
