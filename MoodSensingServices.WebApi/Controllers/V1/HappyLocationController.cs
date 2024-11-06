using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Webapi.Controllers;

namespace MoodSensingServices.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class HappyLocationController: BaseApiController
    {
        private readonly ILogger<HappyLocationController> _logger;

        public HappyLocationController(ILogger<HappyLocationController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get the closest happy location
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClosestHappyLocationAsync([FromQuery] string userId, [FromQuery] string latitude, [FromQuery] string longitude, CancellationToken cancellationToken)
        {
            var moodFrequencyRequest = new GetClosestHappyLocationRequest(userId, latitude, longitude);
            var output = await Mediator.Send(moodFrequencyRequest, cancellationToken).ConfigureAwait(false);
            return Ok(output);
        }
    }
}
