using Microsoft.AspNetCore.Http;
using MoodSensingServices.Application.Entities;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Domain.Extensions;
using System.Drawing;

namespace MoodSensingServices.Application.BusinessLogic
{
    public class UserImageOperationService : IUserImageOperationService
    {
        private readonly IFileService _fileService;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Location> _locationRepository;

        private readonly string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
        private const long MAX_FILE_SIZE = 1 * 1024 * 1024;

        public UserImageOperationService(IFileService fileService, IRepository<User> userRepository, IRepository<Location> locationRepository) 
        {
            _fileService = fileService;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        /// <inheritdoc />
        public async Task<IUploadImageOutputDTO> UploadImageAsync(IUploadImageInputDTO input, CancellationToken cancellationToken)
        {
            try
            {
                if (input.ImageFile?.Length > MAX_FILE_SIZE)
                {
                    throw new BadHttpRequestException("File size should not exceed 1 MB", StatusCodes.Status400BadRequest);
                }

                string createdImageName = await _fileService.SaveFileAsync(input.ImageFile!, allowedFileExtentions);
                var mood = MoodTypeExtension.GetMood();

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserId = input.UserId,
                    Image = createdImageName,
                    Mood = mood,
                    Location = new Location
                    {
                        LocationId = Guid.NewGuid(),
                        Latitude = input.Latitude!,
                        Longitude = input.Longitude!
                    }
                };

                 await _userRepository.Insert(user).ConfigureAwait(false);

                return new UploadImageOutputDTO
                {
                    Mood = mood,
                    MoodType = mood.GetMoodType()
                };
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
        public async Task<Image?> GetUserHappiestImageAsync(Guid userId, CancellationToken cancellationToken)
        {
            var imageFileName = _userRepository.GetById(userId)?.Image;

            Image? result = null;

            if (!string.IsNullOrWhiteSpace(imageFileName)) 
            {
                result = await _fileService.GetFileAsync(imageFileName).ConfigureAwait(false);
            }

            return result;
        }
    }
}
