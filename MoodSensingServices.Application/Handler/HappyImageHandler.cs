using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Requests;

namespace MoodSensingServices.Application.Handler
{
    public class HappyImageHandler: IRequestHandler<GetHappiestImageRequest, FileStreamResult>
    {
        private readonly IUserImageOperationService _userImageOperationService;

        public HappyImageHandler(IUserImageOperationService userImageOperationService)
        {
            _userImageOperationService = userImageOperationService;
        }

        /// <summary>
        /// handler function to get the happiest image of the user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<FileStreamResult> Handle(GetHappiestImageRequest request, CancellationToken cancellationToken)
        {
            return _userImageOperationService.GetUserHappiestImageAsync(request.userId, cancellationToken);
        }
    }
}
