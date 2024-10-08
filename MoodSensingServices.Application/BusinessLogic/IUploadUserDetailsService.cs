using MoodSensingServices.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Application.BusinessLogic
{
    public interface IUploadUserDetailsService
    {
        /// <summary>
        /// Upload user details service
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IUploadImageOutputDTO> UploadImageAsync(IUploadImageInputDTO input, CancellationToken cancellationToken);
    }
}
