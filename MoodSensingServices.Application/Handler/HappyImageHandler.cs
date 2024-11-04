using MediatR;
using MoodSensingServices.Application.BusinessLogic;
using MoodSensingServices.Application.Requests;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodSensingServices.Application.Handler
{
    public class HappyImageHandler: IRequestHandler<GetHappiestImageRequest, Image?>
    {
        private readonly IUserImageOperationService _userImageOperationService;

        public HappyImageHandler(IUserImageOperationService userImageOperationService)
        {
            _userImageOperationService = userImageOperationService;
        }

        /// <summary>
        /// handler function to get the happiest image of the user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
        public async Task<Image?> Handle(GetHappiestImageRequest request, CancellationToken cancellationToken)
        {
            return await _userImageOperationService.GetUserHappiestImageAsync(request.userId, cancellationToken).ConfigureAwait(false);
        }
    }
}
