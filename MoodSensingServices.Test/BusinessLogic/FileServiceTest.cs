using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Test.Base;
using Xunit;

namespace MoodSensingServices.Test.BusinessLogic
{
    public class FileServiceTest
    {
        [Theory, InlineAutoMoqData]
        public async Task SaveFileAsyncError(FileService fileService)
        {
            // Arrange
            var imageFile = MockImageUtility.GetMockImage("test-image.gif", 1024);
            string[] allowedFileExtensions = [".jpg", ".png"];

            // Assert
            var exception = await Assert.ThrowsAsync<BadImageFormatException>(
                async () => await fileService.SaveFileAsync(imageFile, allowedFileExtensions)
            );
        }

        [Theory, InlineAutoMoqData]
        public async Task SaveFileAsyncOk(FileService fileService)
        {
            // Arrange
            var imageFile = MockImageUtility.GetMockImage("test-image.jpg", 1024);
            string[] allowedFileExtensions = [".jpg", ".png"];

            // Act
            var result = await fileService.SaveFileAsync(imageFile,allowedFileExtensions).ConfigureAwait(true);

            // Assert
            Assert.NotNull(result);
        }
    }
}
