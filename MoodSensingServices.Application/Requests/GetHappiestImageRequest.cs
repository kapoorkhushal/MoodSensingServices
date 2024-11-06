using MediatR;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.Requests
{
    public class GetHappiestImageRequest: IRequest<IGetHappiestImageOutputDTO>
    {
        public Guid userId { get; set; }

        public GetHappiestImageRequest(string userId)
        {
            this.userId = Guid.Parse(userId);
        }
    }
}
