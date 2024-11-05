using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MoodSensingServices.Application.Requests
{
    public class GetHappiestImageRequest: IRequest<FileStreamResult>
    {
        public Guid userId { get; set; }

        public GetHappiestImageRequest(string userId)
        {
            this.userId = Guid.Parse(userId);
        }
    }
}
