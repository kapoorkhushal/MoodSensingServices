using MediatR;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Requests;
using MoodSensingServices.Domain.DTOs;

namespace ServiceProviders.Application.Features
{
    public class MoodFrequencyHandler : IRequestHandler<GetAllMoodFrequenciesRequest, List<IGetMoodFrequencyOutputDTO>>
    {
        private readonly IMoodOperationService _moodOperationService;

        public MoodFrequencyHandler(IMoodOperationService moodOperationService)
        {
            _moodOperationService = moodOperationService;
        }

        /// <summary>
        /// handler function to get list of mood frequencies for the input user id
        /// </summary>
        /// <param name="moodFrequenciesRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<IGetMoodFrequencyOutputDTO>> Handle(GetAllMoodFrequenciesRequest moodFrequenciesRequest, CancellationToken cancellationToken)
        {
            return await _moodOperationService.GetMoodFrequenciesAsync(moodFrequenciesRequest.userId).ConfigureAwait(false);
        }
    }
}
