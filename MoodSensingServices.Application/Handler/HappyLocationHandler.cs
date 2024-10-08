using MediatR;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.Handler
{
    public class HappyLocationHandler : IRequestHandler<GetClosestHappyLocationRequest, IGetClosestHappyLocationOutputDTO?>
    {
        private readonly ILocationService _locationService;

        public HappyLocationHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// handler function to get the closest happy location for the input user details
        /// </summary>
        /// <param name="closestHappyLocationRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IGetClosestHappyLocationOutputDTO?> Handle(GetClosestHappyLocationRequest closestHappyLocationRequest, CancellationToken cancellationToken)
        {
            return await _locationService.GetClosestHappyMoodAsync(closestHappyLocationRequest.userId, closestHappyLocationRequest.latitude, closestHappyLocationRequest.longitude).ConfigureAwait(false);
        }
    }
}
