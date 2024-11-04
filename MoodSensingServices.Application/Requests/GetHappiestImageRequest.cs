using MediatR;
using System.Drawing;

namespace MoodSensingServices.Application.Requests
{
    public class GetHappiestImageRequest: IRequest<Image>
    {
        public Guid userId { get; set; }

        public GetHappiestImageRequest(string userId)
        {
            this.userId = Guid.Parse(userId);
        }
    }
}
