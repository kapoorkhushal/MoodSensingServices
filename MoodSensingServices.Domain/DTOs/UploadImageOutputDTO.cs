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
    public class UploadImageOutputDTO : IUploadImageOutputDTO
    {
        /// <summary>
        /// Mood frequency
        /// </summary>
        [DataMember(Name = "Mood")]
        public int Mood { get; set; }

        /// <summary>
        /// Mood Type
        /// </summary>
        [DataMember(Name = "MoodType")]
        public string? MoodType { get; set; }
    }
}
