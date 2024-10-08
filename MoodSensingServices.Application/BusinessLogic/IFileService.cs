using Microsoft.AspNetCore.Http;

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
