using MediatR;

namespace MoodSensingServices.Application.Requests
{
    public class GetHappiestImageRequest: IRequest<string>
    {
        public Guid userId { get; set; }

        public GetHappiestImageRequest(string userId)
        {
            this.userId = Guid.Parse(userId);
        }
    }
}
