using Microsoft.AspNetCore.Http;
using MoodSensingServices.Application.Entities;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Domain.Extensions;

namespace MoodSensingServices.Application.BusinessLogic
{
    public class MoodOperationService : IMoodOperationService
    {
        private readonly IRepository<User> _userRepository;
        public MoodOperationService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<IList<IGetMoodFrequencyOutputDTO>> GetMoodFrequenciesAsync(Guid userId)
        {
            var userDetails = await _userRepository.GetAll(user => user.UserId == userId).ConfigureAwait(false);

            if(userDetails is null || userDetails.Count == 0)
            {
                throw new BadHttpRequestException("User not found");
            }

            var output = new List<IGetMoodFrequencyOutputDTO>();
            foreach (var userDetail in userDetails) 
            {
                output.Add(
                    new GetMoodFrequencyOutputDTO()
                    {
                        MoodType = userDetail.Mood.GetMoodType(),
                        Latitude = userDetail.Location.Latitude,
                        Longitude = userDetail.Location.Longitude
                    });
            }

            return output;
        }
    }
}
