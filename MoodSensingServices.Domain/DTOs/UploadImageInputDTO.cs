using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Domain.DTOs
{
    public class UploadImageInputDTO: IUploadImageInputDTO
    {
        /// <summary>
        /// User Id
        /// </summary>
        [DataMember(Name = "UserId")]
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "Latitude")]
        [Required]
        public string? Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "Longitude")]
        [Required]
        public string? Longitude { get; set; }

        /// <summary>
        /// Image File
        /// </summary>
        [DataMember(Name = "ImageFile")]
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
}
