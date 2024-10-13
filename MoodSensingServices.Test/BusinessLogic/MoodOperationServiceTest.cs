using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Entities;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Test.Base;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace MoodSensingServices.Test.BusinessLogic
{
    public class MoodOperationServiceTest
    {
        [Theory, InlineAutoMoqData]
        public async Task IsGetMoodFrequenciesAsyncOk(
            IList<User> users,
            [Frozen] Mock<IRepository<User>> _userRepository,
            MoodOperationService moodOperationService)
        {
            // Arrange
            _userRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users);

            // Act
            var output = await moodOperationService.GetMoodFrequenciesAsync(It.IsAny<Guid>()).ConfigureAwait(true);

            // Assert
            _userRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            Assert.Equal(users.Count, output.Count);
        }

        [Theory, InlineAutoMoqData]
        public async Task IsGetMoodFrequenciesAsyncNOk(
            [Frozen] Mock<IRepository<User>> _userRepository,
            MoodOperationService moodOperationService)
        {
            // Arrange
            _userRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync([]);

            // Assert
            var exception = await Assert.ThrowsAsync<BadHttpRequestException>(
                async () => await moodOperationService.GetMoodFrequenciesAsync(It.IsAny<Guid>())
            );
            _userRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            Assert.Equal(exception?.StatusCode, StatusCodes.Status400BadRequest);
            Assert.Equal(exception?.Message, "User not found");
        }
    }
}
