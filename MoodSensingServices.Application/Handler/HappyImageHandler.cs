using MediatR;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.Handler
{
    public class HappyImageHandler: IRequestHandler<GetHappiestImageRequest, IGetHappiestImageOutputDTO>
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
        public async Task<IGetHappiestImageOutputDTO> Handle(GetHappiestImageRequest request, CancellationToken cancellationToken)
        {
            var imageUrl = await _userImageOperationService.GetUserHappiestImageAsync(request.userId, cancellationToken).ConfigureAwait(false);

            return new GetHappiestImageOutputDTO
            {
                ImagePath = imageUrl
            };
        }
    }
}
