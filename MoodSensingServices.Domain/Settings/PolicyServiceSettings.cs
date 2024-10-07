namespace MoodSensingServices.Domain.Settings
{
    public class PolicyServiceSettings
    {
        public int? TimeoutInSeconds {  get; set; }
        public int? BackOffDelayInMilliseconds {  get; set; }
        public int? RetryCount {  get; set; }
    }
}
