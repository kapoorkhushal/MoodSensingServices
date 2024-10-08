using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Domain.DTOs
{
    public interface IGetMoodFrequencyOutputDTO
    {
        /// <summary>
        /// Latitude
        /// </summary>
        string? Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        string? Longitude { get; set; }

        /// <summary>
        /// Mood Type
        /// </summary>
        string? MoodType { get; set; }
    }
}
