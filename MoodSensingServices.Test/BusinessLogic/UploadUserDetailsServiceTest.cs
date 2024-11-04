using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Entities;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Test.Base;
using Moq;
using Xunit;

namespace MoodSensingServices.Test.BusinessLogic
{
    public class UploadUserDetailsServiceTest
    {
        [Theory, InlineAutoMoqData]
        public async Task IsUploadImageAsyncOk(
            [Frozen] Mock<IFileService> _fileService,
            [Frozen] Mock<IRepository<User>> _userRepository,
            UserImageOperationService service)
        {
            // Arrange
            IUploadImageInputDTO input = new UploadImageInputDTO
            {
                ImageFile = MockImageUtility.GetMockImage("test-file.jpg", 1024),
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
            UserImageOperationService service)
        {
            // Arrange
            IUploadImageInputDTO input = new UploadImageInputDTO
            {
                ImageFile = MockImageUtility.GetMockImage("test-file.jpg", 1024*1024*2),
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
    }
}
