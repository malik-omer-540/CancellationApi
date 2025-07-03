using CancellationApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CancellationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILongTaskService _longTaskService;

        public TaskController(ILongTaskService longTaskService)
        {
            _longTaskService = longTaskService;
        }

        /// <summary>
        /// Long-running task with proper cancellation support
        /// </summary>
        [HttpGet("long-task")]
        public async Task<IActionResult> LongRunningTask(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _longTaskService.RunLongTaskAsync(cancellationToken);
                return Ok(result);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Client closed request");
            }
        }

        /// <summary>
        /// Bad: Task runs fully even if request is cancelled
        /// </summary>
        [HttpGet("long-task-uncancellable")]
        public async Task<IActionResult> LongTaskWithoutCancellation()
        {
            await Task.Delay(5000); // Ignoring cancellation
            return Ok("Task Completed without cancellation support");
        }
    }

}
