using MoodSensingServices.Domain.Extensions;
using Xunit;

namespace MoodSensingServices.Test.Extensions
{
    public class MoodTypeExtensionsTest
    {

        [Theory]
        [InlineData(10, "SAD :(")]
        [InlineData(25, "SAD :(")]
        [InlineData(35, "SAD :(")]
        [InlineData(36, "NEUTRAL :|")]
        [InlineData(50, "NEUTRAL :|")]
        [InlineData(65, "NEUTRAL :|")]
        [InlineData(66, "HAPPY :)")]
        [InlineData(75, "HAPPY :)")]
        [InlineData(100, "HAPPY :)")]
        public void IsGetMoodTypeOk(int moodValue, string expectedResult)
        {
            var result = moodValue.GetMoodType();

            Assert.Equal(expectedResult, result);
        }
    }
}
