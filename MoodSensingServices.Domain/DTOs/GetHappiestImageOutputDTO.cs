using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Domain.DTOs
{
    /// <summary>
    /// Upload Image & location output DTO
    /// </summary>
    public class GetHappiestImageOutputDTO : IGetHappiestImageOutputDTO
    {
        /// <summary>
        /// Image Path
        /// </summary>
        [DataMember(Name = "ImagePath")]
        [Required]
        public string? ImagePath { get; set; }
    }
}
