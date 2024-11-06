using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Domain.DTOs
{
    public interface IGetHappiestImageOutputDTO
    {
        /// <summary>
        /// Image Path
        /// </summary>
        string? ImagePath { get; set; }
    }
}
