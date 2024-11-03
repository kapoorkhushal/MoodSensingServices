using Microsoft.AspNetCore.Http;
using Moq;

namespace MoodSensingServices.Test.Base
{
    public static class MockImageUtility
    {
        /// <summary>
        /// get mock image data
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="length"></param>
        /// <returns> IFormFile </returns>
        public static IFormFile GetMockImage(string fileName, long length)
        {
            var imageContent = new byte[] { 137, 81, 96, 42, 89, 66, 10, 26, 10 };
            var memoryStream = new MemoryStream(imageContent);
            var mockFormFile = new Mock<IFormFile>();

            mockFormFile.Setup(f => f.FileName).Returns(fileName);
            mockFormFile.Setup(f => f.Length).Returns(length);
            mockFormFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            mockFormFile.Setup(f => f.ContentDisposition).Returns($"form-data; name=\"file\"; filename=\"{fileName}\"");
            mockFormFile.Setup(f => f.ContentType).Returns("image/jpg");

            return mockFormFile.Object;
        }
    }
}
