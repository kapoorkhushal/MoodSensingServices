﻿using Microsoft.AspNetCore.Http;
using MoodSensingServices.Application.Entities;
using MoodSensingServices.Application.Interfaces;
using MoodSensingServices.Domain.Constants;
using MoodSensingServices.Domain.DTOs;
using MoodSensingServices.Domain.Extensions;

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
        public async Task<string> GetUserHappiestImageAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userDetails = await _userRepository.GetAll(user => user.UserId == userId).ConfigureAwait(false);
            var happiestImageFileName = userDetails
                ?.Where(user => string.Equals(user.Mood.GetMoodType(), MoodTypeConstants.Happy))
                .MaxBy(x => x.Mood)
                ?.Image;

            if (string.IsNullOrWhiteSpace(happiestImageFileName)) 
            {
                throw new BadHttpRequestException("No happiest Image found");
            }

            return _fileService.GetFileAddress(happiestImageFileName);
        }
    }
}
