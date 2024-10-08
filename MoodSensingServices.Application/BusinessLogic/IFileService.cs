using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface IFileService
    {
        /// <summary>
        /// save file & get converted file name
        /// </summary>
        /// <param name="file"></param>
        /// <param name="allowedExtensions"></param>
        /// <returns></returns>
        Task<string> SaveFileAsync(IFormFile file, string[] allowedExtensions);
    }
}
