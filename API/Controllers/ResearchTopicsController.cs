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
    public class ResearchTopicsController : ControllerBase
    {
        private readonly IResearchTopicService _researchTopicService;

        public ResearchTopicsController(IResearchTopicService researchTopicService)
        {
            _researchTopicService = researchTopicService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy thông tin chi tiết 1 đề tài nghiên cứu theo ID")]
        public async Task<ActionResult<ResearchTopicDto>> GetById(int id)
        {
            var researchTopic = await _researchTopicService.GetByIdAsync(id);
            if (researchTopic == null)
                return NotFound();

            return Ok(researchTopic);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResearchTopicDto>>> GetAll()
        {
            var researchTopics = await _researchTopicService.GetAllAsync();
            return Ok(researchTopics);
        }

        [HttpGet("active")]
        [SwaggerOperation(Summary = "Lấy danh sách đề tài nghiên cứu đang hoạt động")]
        public async Task<ActionResult<IEnumerable<ResearchTopicDto>>> GetActive()
        {
            var researchTopics = await _researchTopicService.GetActiveAsync();
            return Ok(researchTopics);
        }

        [HttpGet("created-by/{userId}")]
        [SwaggerOperation(Summary = "Lấy danh sách đề tài nghiên cứu theo ID người tạo")]
        public async Task<ActionResult<IEnumerable<ResearchTopicDto>>> GetByCreatedById(int userId)
        {
            var researchTopics = await _researchTopicService.GetByCreatedByIdAsync(userId);
            return Ok(researchTopics);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Tạo mới đề tài nghiên cứu")]
        [Authorize(Roles = "HostInstitution,Admin")]
        public async Task<ActionResult<ResearchTopicDto>> Create([FromBody] CreateResearchTopicRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var researchTopic = await _researchTopicService.CreateAsync(request, userId);
            if (researchTopic == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = researchTopic.Id }, researchTopic);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Cập nhật thông tin đề tài nghiên cứu")]
        [Authorize(Roles = "HostInstitution,Admin")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateResearchTopicRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _researchTopicService.UpdateAsync(id, request, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xóa đề tài nghiên cứu theo ID")]
        [Authorize(Roles = "HostInstitution,Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _researchTopicService.DeleteAsync(id, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpPatch("{id}/toggle-active")]
        [SwaggerOperation(Summary = "Bật/tắt trạng thái hoạt động của một đề tài")]
        [Authorize(Roles = "HostInstitution,Admin")]
        public async Task<ActionResult> ToggleActiveStatus(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _researchTopicService.ToggleActiveStatusAsync(id, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }
    }
}
