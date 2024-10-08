using MediatR;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.Requests
{
    public class GetClosestHappyLocationRequest : IRequest<IGetClosestHappyLocationOutputDTO?>
    {
        public Guid userId { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public GetClosestHappyLocationRequest(string userId, string latitude, string longitude)
        {
            this.userId = Guid.Parse(userId);
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
