using MoodSensingServices.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface IMoodOperationService
    {
        /// <summary>
        /// Get list of mood frequencies as per input user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<IGetMoodFrequencyOutputDTO>> GetMoodFrequencies(Guid userId);
    }
}
