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
            var users = await _userRepository.GetAll(user => user.UserId == userId).ConfigureAwait(false);

            if(users is null || users.Count == 0)
            {
                throw new BadHttpRequestException("User not found");
            }

            var output = new List<IGetMoodFrequencyOutputDTO>();
            foreach (var user in users) 
            {
                output.Add(
                    new GetMoodFrequencyOutputDTO()
                    {
                        MoodType = user.Mood.GetMoodType(),
                        Latitude = user.Location.Latitude,
                        Longitude = user.Location.Longitude
                    });
            }

            return output;
        }
    }
}
