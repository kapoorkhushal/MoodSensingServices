using AutoFixture.Xunit2;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Domain.Constants;
using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Test.Base;
using Moq;
using Xunit;

namespace MoodSensingServices.Test.BusinessLogic
{
    public class LocationServiceTest
    {
        [Theory, InlineAutoMoqData("7977a31f-d669-451b-ab9f-c9a1f90f9b70", "50", "50")]
        public async Task IsGetMoodFrequenciesAsyncOk(
            Guid userId,
            string latitude,
            string longitude,
            [Frozen] Mock<IMoodOperationService> _moodOperationService,
            LocationService locationService)
        {
            // Arrange
            IList<IGetMoodFrequencyOutputDTO> users = GetMockUsers();
            _moodOperationService.Setup(x => x.GetMoodFrequenciesAsync(userId)).ReturnsAsync(users);

            // Act
            var output = await locationService.GetClosestHappyMoodAsync(userId, latitude, longitude).ConfigureAwait(true);

            // Assert
            _moodOperationService.Verify(x => x.GetMoodFrequenciesAsync(userId), Times.Once);
            Assert.Equal(output?.Latitude, users[2].Latitude);
            Assert.Equal(output?.Longitude, users[2].Longitude);
            Assert.Equal(output?.MoodType, users[2].MoodType);
        }

        private IList<IGetMoodFrequencyOutputDTO> GetMockUsers()
        {
            return
            [
                new GetMoodFrequencyOutputDTO {
                    MoodType = MoodTypeConstants.Happy,
                    Latitude = "102",
                    Longitude = "22"
                },
                new GetMoodFrequencyOutputDTO {
                    MoodType = MoodTypeConstants.Neutral,
                    Latitude = "52",
                    Longitude = "60"
                },
                new GetMoodFrequencyOutputDTO {
                    MoodType = MoodTypeConstants.Happy,
                    Latitude = "52",
                    Longitude = "62"
                },
                new GetMoodFrequencyOutputDTO {
                    MoodType = MoodTypeConstants.Sad,
                    Latitude = "32",
                    Longitude = "62"
                },
                new GetMoodFrequencyOutputDTO {
                    MoodType = MoodTypeConstants.Sad,
                    Latitude = "100",
                    Longitude = "100"
                }
            ];
        }
    }
}
