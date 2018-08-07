using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Utilities
{
    public interface IImageUploader
    {
        string UploadImageFromForm(IFormFile imageFile);
    }
}