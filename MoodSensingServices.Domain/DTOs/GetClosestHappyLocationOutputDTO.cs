using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Domain.DTOs
{
    /// <summary>
    /// Upload Image & location output DTO
    /// </summary>
    public class GetClosestHappyLocationOutputDTO : IGetClosestHappyLocationOutputDTO
    {
        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "Latitude")]
        public string? Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "Longitude")]
        public string? Longitude { get; set; }

        /// <summary>
        /// Mood Type
        /// </summary>
        [DataMember(Name = "MoodType")]
        public string? MoodType { get; set; }
    }
}
