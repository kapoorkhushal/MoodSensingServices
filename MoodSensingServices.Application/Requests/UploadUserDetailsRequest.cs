using MediatR;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.Requests
{
    public class UploadUserDetailsRequest : IRequest<IUploadImageOutputDTO>
    {
        public IUploadImageInputDTO input { get; set; }

        public UploadUserDetailsRequest(IUploadImageInputDTO input)
        {
            this.input = input;
        }
    }
}
