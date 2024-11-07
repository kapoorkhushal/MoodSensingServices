using MoodSensingServices.Domain.Constants;

namespace MoodSensingServices.Domain.Extensions
{
    public static class MoodTypeExtension
    {
        /// <summary>
        /// get mood type as per the input mood value
        /// </summary>
        /// <param name="moodValue"></param>
        /// <returns>returns mood type string</returns>
        public static string GetMoodType(this int moodValue)
        {
            string result = moodValue switch
            {
                int n when (n >= 0 && n <= 35) => MoodTypeConstants.Sad,
                int n when (n > 35 && n <= 65) => MoodTypeConstants.Neutral,
                int n when (n > 65 && n <= 100) => MoodTypeConstants.Happy,
                _ => string.Empty
            };

            return result;
        }

        /// <summary>
        /// Generate random number between 0 to 100
        /// </summary>
        /// <returns>random integer number</returns>
        public static int GetMood()
        {
            Random rng = new Random();

            // number between 0 and 99
            return rng.Next(100); 
        }
    }
}
