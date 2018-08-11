using AutoMapper;
using BusinessLogicLayer.Utilities;
using Models.DataTransferObjects.Creating;
using Models.Entities;

namespace BusinessLogicLayer.Mappings.Resolvers
{
    public class FormImageToUrlResolver : IValueResolver<UserRegistrationDto, User, string>
    {
        private readonly IImageUploader _imageUploader;

        public FormImageToUrlResolver(IImageUploader imageUploader) => _imageUploader = imageUploader;

        public string Resolve(UserRegistrationDto source, User destination, string destMember, ResolutionContext context) =>
            _imageUploader.UploadImageFromForm(source.Avatar);
    }
}