using BusinessLogicLayer.Utilities.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Utilities
{
    public class CloudinaryImageUploader : IImageUploader
    {
        private readonly CloudinarySettings _cloudinarySettings;

        public CloudinaryImageUploader(IOptions<CloudinarySettings> cloudinaryOptions) => _cloudinarySettings = cloudinaryOptions.Value;

        public string UploadImageFromForm(IFormFile imageFile) => new Cloudinary(new Account(_cloudinarySettings.Cloud,
                                                                                             _cloudinarySettings.ApiKey,
                                                                                             _cloudinarySettings.ApiSecret))
                                                                 .Upload(new ImageUploadParams
                                                                  {
                                                                      File = new FileDescription(imageFile.FileName,
                                                                                                 imageFile.OpenReadStream())
                                                                  }).SecureUri.AbsoluteUri;
    }
}