using MediatR;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.Handler
{
    public class UploadDetailsHandler : IRequestHandler<UploadUserDetailsRequest, IUploadImageOutputDTO?>
    {
        private readonly IUserImageOperationService _uploadUserDetailsService;
        public UploadDetailsHandler(IUserImageOperationService uploadUserDetailsService)
        {
            _uploadUserDetailsService = uploadUserDetailsService;
        }

        /// <summary>
        /// handler function to upload user details
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IUploadImageOutputDTO?> Handle(UploadUserDetailsRequest request, CancellationToken cancellationToken)
        {
            return await _uploadUserDetailsService.UploadImageAsync(request.input, cancellationToken).ConfigureAwait(false);
        }
    }
}
