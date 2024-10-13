using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Domain.Mapper
{
    public static class ClosestHappyLocationMapper
    {
        /// <summary>
        /// map Mood frequency output dto to closest happy location output dto
        /// </summary>
        /// <param name="moodFrequency"></param>
        /// <returns></returns>
        public static IGetClosestHappyLocationOutputDTO GetClosestHappyLocationOutput(this IGetMoodFrequencyOutputDTO moodFrequency)
        {
            return new GetClosestHappyLocationOutputDTO
            {
                MoodType = moodFrequency.MoodType,
                Latitude = moodFrequency.Latitude,
                Longitude = moodFrequency.Longitude
            };
        }
    }
}
