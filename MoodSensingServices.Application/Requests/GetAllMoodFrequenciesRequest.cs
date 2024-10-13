using MediatR;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.Requests
{
    public class GetAllMoodFrequenciesRequest: IRequest<IList<IGetMoodFrequencyOutputDTO>>
    {
        public Guid userId { get; set; }

        public GetAllMoodFrequenciesRequest(string userId)
        {
            this.userId = Guid.Parse(userId);
        }
    }
}
