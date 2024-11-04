using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Webapi.Controllers;
using System.Security.Principal;

namespace MoodSensingServices.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class HappyImageController: BaseApiController
    {
        private readonly ILogger<HappyImageController> _logger;

        public HappyImageController(ILogger<HappyImageController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get the most happiest image for the input user id
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Now Each system has WINDOWS > 6.0")]
        public async Task<IActionResult> GetHappiestImageAsync([FromQuery] string userId, CancellationToken cancellationToken)
        {
            var happiestImageRequest = new GetHappiestImageRequest(userId);
            var output = await Mediator.Send(happiestImageRequest, cancellationToken).ConfigureAwait(false);
            return Ok(output);
        }
    }
}
