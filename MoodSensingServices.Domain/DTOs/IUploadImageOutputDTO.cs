using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Domain.DTOs
{
    public interface IUploadImageOutputDTO
    {
        /// <summary>
        /// Mood frequency
        /// </summary>
        int Mood { get; set; }

        /// <summary>
        /// Mood Type
        /// </summary>
        string? MoodType { get; set; }
    }
}
