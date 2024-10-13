using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Entities;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Test.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoodSensingServices.Test.BusinessLogic
{
    public class UploadUserDetailsServiceTest
    {
        [Theory, InlineAutoMoqData]
        public async Task IsUploadImageAsyncOk(
            [Frozen] Mock<IFileService> _fileService,
            [Frozen] Mock<IRepository<User>> _userRepository,
            UploadUserDetailsService service)
        {
            // Arrange
            IUploadImageInputDTO input = new UploadImageInputDTO
            {
                ImageFile = GetMockImage("test-file.jpg", 1024),
                UserId = Guid.NewGuid(),
                Latitude = "100",
                Longitude = "100"
            };

            string[] allowedExtensions = [".jpg"];

            // Act
            var output = await service.UploadImageAsync(input, CancellationToken.None).ConfigureAwait(true);

            // Assert
            _userRepository.Verify(x => x.Insert(It.IsAny<User>()), Times.Once);
            _fileService.Verify(x => x.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string[]>()), Times.Once);
        }

        [Theory, InlineAutoMoqData]
        public async Task IsUploadImageFileSizeNOkAsync(
            [Frozen] Mock<IFileService> _fileService,
            [Frozen] Mock<IRepository<User>> _userRepository,
            UploadUserDetailsService service)
        {
            // Arrange
            IUploadImageInputDTO input = new UploadImageInputDTO
            {
                ImageFile = GetMockImage("test-file.jpg", 1024*1024*2),
                UserId = Guid.NewGuid(),
                Latitude = "100",
                Longitude = "100"
            };

            string[] allowedExtensions = [".jpg"];

            // Assert
            var exception = await Assert.ThrowsAsync<BadHttpRequestException>(
                async () => await service.UploadImageAsync(input, CancellationToken.None)
            );

            Assert.Equal(StatusCodes.Status400BadRequest, exception?.StatusCode);
            Assert.Equal("File size should not exceed 1 MB", exception?.Message);
            _userRepository.Verify(x => x.Insert(It.IsAny<User>()), Times.Never);
            _fileService.Verify(x => x.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string[]>()), Times.Never);
        }

        /// <summary>
        /// get mock image data
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="length"></param>
        /// <returns> IFormFile </returns>
        private IFormFile GetMockImage(string fileName, long length)
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
