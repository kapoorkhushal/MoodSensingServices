using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface IMoodOperationService
    {
        /// <summary>
        /// Get list of mood frequencies as per input user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<IGetMoodFrequencyOutputDTO>> GetMoodFrequenciesAsync(Guid userId);
    }
}
