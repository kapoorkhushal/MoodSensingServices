using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Webapi.Controllers;

namespace MoodSensingServices.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class MoodFrequencyController: BaseApiController
    {
        private readonly ILogger<MoodFrequencyController> _logger;

        public MoodFrequencyController(ILogger<MoodFrequencyController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get the list of mood frequencies
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMoodFrequencies([FromQuery] string userId, CancellationToken cancellationToken)
        {
            var moodFrequencyRequest = new GetAllMoodFrequenciesRequest(userId);
            var output = await Mediator.Send(moodFrequencyRequest, cancellationToken).ConfigureAwait(false);
            return Ok(output);
        }
    }
}
