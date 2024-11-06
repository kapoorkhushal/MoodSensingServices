using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoodSensingServices.Domain.Extensions;

namespace MoodSensingServices.Application.BusinessLogic
{
    public class FileService: IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _path;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
            _path = GetPath();
        }

        /// <inheritdoc />
        public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            // Check the allowed extenstions
            var extension = Path.GetExtension(imageFile.FileName);
            if (!allowedFileExtensions.Contains(extension))
            {
                throw new BadImageFormatException($"Only {string.Join(", ", allowedFileExtensions)} are allowed.");
            }

            // generate a unique filename
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fileNameWithPath = Path.Combine(_path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream).ConfigureAwait(false);
            return fileName;
        }

        /// <inheritdoc />
        public FileStreamResult GetFileStream(string fileName)
        {
            // Combine the file name from the database with the uploads folder path
            string imagePath = Path.Combine(_path, fileName);

            if (!Directory.Exists(_path))
            {
                throw new BadHttpRequestException($"directory: {_path} does not exist", new DirectoryNotFoundException());
            }
            else if (!File.Exists(imagePath))
            {
                throw new BadHttpRequestException($"file: {imagePath} does not exist", new FileNotFoundException());
            }

            // Open a file stream to the file
            var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, fileName.GetContentType());
        }

        /// <inheritdoc />
        public string GetFileAddress(string fileName)
        {
            // Combine the file name from the database with the uploads folder path
            return string.Concat("http://localhost:5000/Uploads/", fileName);
        }

        /// <summary>
        /// returns the absolute path of the directory which will contain the content
        /// </summary>
        /// <returns></returns>
        private string GetPath()
        {
            var contentPath = _environment.ContentRootPath;
            return Path.Combine(contentPath, "Uploads");
        }
    }
}
