using Microsoft.AspNetCore.Mvc;
using MoodSensingServices.Domain.DTOs;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface IUserImageOperationService
    {
        /// <summary>
        /// Upload user details service
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>returns uploaded user details</returns>
        Task<IUploadImageOutputDTO> UploadImageAsync(IUploadImageInputDTO input, CancellationToken cancellationToken);

        /// <summary>
        /// Get the user happiest image
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>returns the most happiest image of the user</returns>
        Task<FileStreamResult> GetUserHappiestImageAsync(Guid userId, CancellationToken cancellationToken);
    }
}
