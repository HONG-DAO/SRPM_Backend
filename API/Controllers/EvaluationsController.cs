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
    public class EvaluationsController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationsController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy đánh giá theo ID")]
        public async Task<ActionResult<EvaluationDto>> GetById(int id)
        {
            var evaluation = await _evaluationService.GetByIdAsync(id);
            if (evaluation == null)
                return NotFound();

            return Ok(evaluation);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationDto>>> GetAll()
        {
            var evaluations = await _evaluationService.GetAllAsync();
            return Ok(evaluations);
        }

        [HttpGet("project/{projectId}")]
        [SwaggerOperation(Summary = "Lấy đánh giá theo ID dự án")]
        public async Task<ActionResult<IEnumerable<EvaluationDto>>> GetByProjectId(int projectId)
        {
            var evaluations = await _evaluationService.GetByProjectIdAsync(projectId);
            return Ok(evaluations);
        }

        [HttpGet("task/{taskId}")]
        [SwaggerOperation(Summary = "Lấy đánh giá theo ID nhiệm vụ")]
        public async Task<ActionResult<IEnumerable<EvaluationDto>>> GetByTaskId(int taskId)
        {
            var evaluations = await _evaluationService.GetByTaskIdAsync(taskId);
            return Ok(evaluations);
        }

        [HttpGet("evaluated-by/{userId}")]
        [SwaggerOperation(Summary = "Lấy đánh giá theo ID người được đánh giá")]
        public async Task<ActionResult<IEnumerable<EvaluationDto>>> GetByEvaluatedById(int userId)
        {
            var evaluations = await _evaluationService.GetByEvaluatedByIdAsync(userId);
            return Ok(evaluations);
        }

        [HttpGet("type/{type}")]
        [SwaggerOperation(Summary = "Lấy đánh giá theo loại")]
        public async Task<ActionResult<IEnumerable<EvaluationDto>>> GetByType(string type)
        {
            var evaluations = await _evaluationService.GetByTypeAsync(type);
            return Ok(evaluations);
        }

        [HttpPost]
        [Authorize(Roles = "AppraisalCouncil,Admin")]
        public async Task<ActionResult<EvaluationDto>> Create([FromBody] CreateEvaluationRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var evaluation = await _evaluationService.CreateAsync(request, userId);
            if (evaluation == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = evaluation.Id }, evaluation);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Cập nhật đánh giá")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateEvaluationRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _evaluationService.UpdateAsync(id, request, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xóa đánh giá")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _evaluationService.DeleteAsync(id, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }
    }
}
