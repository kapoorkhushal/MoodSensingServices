﻿using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface IMoodOperationService
    {
        /// <summary>
        /// Get list of mood frequencies as per input user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns list of location coordinates & it's corresponding mood</returns>
        Task<IList<IGetMoodFrequencyOutputDTO>> GetMoodFrequenciesAsync(Guid userId);
    }
}
