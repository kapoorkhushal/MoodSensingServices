using MediatR;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Requests;

namespace MoodSensingServices.Application.Handler
{
    public class HappyImageHandler: IRequestHandler<GetHappiestImageRequest, string>
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
        public async Task<string> Handle(GetHappiestImageRequest request, CancellationToken cancellationToken)
        {
            return await _userImageOperationService.GetUserHappiestImageAsync(request.userId, cancellationToken).ConfigureAwait(false);
        }
    }
}
