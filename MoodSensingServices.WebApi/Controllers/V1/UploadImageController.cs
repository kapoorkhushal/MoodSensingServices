using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Webapi.Controllers;

namespace MoodSensingServices.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class UploadImageController: BaseApiController
    {
        private readonly ILogger<UploadImageController> _logger;

        public UploadImageController(ILogger<UploadImageController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// upload user image & location details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadDetailsAsync([FromForm] UploadImageInputDTO input, CancellationToken cancellationToken)
        {
            var uploadUserDetailsRequest = new UploadUserDetailsRequest(input);
            var output = await Mediator.Send(uploadUserDetailsRequest, cancellationToken).ConfigureAwait(false);
            return Ok(output);
        }
    }
}
