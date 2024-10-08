namespace MoodSensingServices.Domain.Settings
{
    public class AuthorizationSettings
    {
        public string? ValidIssuer {  get; set; }
        public string? ValidAudience {  get; set; }
        public string? SecretKey {  get; set; }
    }
}
