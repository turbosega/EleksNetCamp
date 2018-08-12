using BusinessLogicLayer.Utilities.Interfaces;
using BusinessLogicLayer.Utilities.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Utilities.Implementations
{
    public class CloudinaryImageUploader : IImageUploader
    {
        private readonly CloudinarySettings _cloudinarySettings;

        public CloudinaryImageUploader(IOptions<CloudinarySettings> cloudinaryOptions) => _cloudinarySettings = cloudinaryOptions.Value;

        public string UploadImageFromForm(IFormFile imageFile) => _cloudinarySettings.Cloudinary
                                                                                     .Upload(new ImageUploadParams
                                                                                      {
                                                                                          File = new FileDescription(imageFile.FileName,
                                                                                                                     imageFile.OpenReadStream())
                                                                                      }).SecureUri.AbsoluteUri;
    }
}