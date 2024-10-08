﻿using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Domain.Mapper;

namespace MoodSensingServices.Application.BusinessLogic
{
    public class LocationService : ILocationService
    {
        private readonly IMoodOperationService _moodOperationService;
        public LocationService(IMoodOperationService moodOperationService)
        {
            _moodOperationService = moodOperationService;
        }

        /// <inheritdoc />
        public async Task<IGetClosestHappyLocationOutputDTO> GetClosestHappyMood(Guid userId, string latitude, string longitude)
        {
            var userMoodFrequency = await _moodOperationService.GetMoodFrequencies(userId).ConfigureAwait(false);

            return userMoodFrequency.OrderBy(x => GetMinDistance(double.Parse(latitude), double.Parse(longitude), double.Parse(x.Latitude), double.Parse(x.Longitude))).First().GetClosestHappyLocationOutput();
        }

        /// <summary>
        /// Get minimum distance b/w two coordinates
        /// </summary>
        /// <param name="latitude1"></param>
        /// <param name="longitude1"></param>
        /// <param name="latitude2"></param>
        /// <param name="longitude2"></param>
        /// <returns></returns>
        private double GetMinDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            return Math.Sqrt(Math.Pow(latitude2 - latitude1, 2) + Math.Pow(longitude2 - longitude1, 2));
        }
    }
}