using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Domain.DTOs
{
    public interface IUploadImageInputDTO
    {
        /// <summary>
        /// User Id
        /// </summary>
        Guid UserId { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        string? Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        string? Longitude { get; set; }

        /// <summary>
        /// Image File
        /// </summary>
        IFormFile? ImageFile { get; set; }
    }
}
