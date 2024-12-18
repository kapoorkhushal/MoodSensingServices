﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface IFileService
    {
        /// <summary>
        /// save file & get converted file name
        /// </summary>
        /// <param name="file"></param>
        /// <param name="allowedExtensions"></param>
        /// <returns>returns the name of the newly inserted file as guid</returns>
        Task<string> SaveFileAsync(IFormFile file, string[] allowedExtensions);

        /// <summary>
        /// get file for the input file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>returns the file by fetching the file for the input file name</returns>
        FileStreamResult GetFileStream(string fileName);

        /// <summary>
        /// get file location for the input file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>returns the full file path by for the input file name</returns>
        string GetFileAddress(string fileName);
    }
}
