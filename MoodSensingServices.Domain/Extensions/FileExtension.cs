namespace MoodSensingServices.Domain.Extensions
{
    public static class FileExtension
    {
        /// <summary>
        /// returns the content type of the input file as per the file extension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>returns file content type</returns>
        public static string GetContentType(this string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();

            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream" // Default content type if unknown
            };
        }
    }
}
