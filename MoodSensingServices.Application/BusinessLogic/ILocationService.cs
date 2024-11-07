using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface ILocationService
    {
        /// <summary>
        /// Get closest happy mood
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>returns the closest happiest locations coordinates</returns>
        Task<IGetClosestHappyLocationOutputDTO?> GetClosestHappyMoodAsync(Guid userId, string latitude, string longitude);
    }
}
