using CloudinaryDotNet;

namespace BusinessLogicLayer.Utilities.Settings
{
    public sealed class CloudinarySettings
    {
        public string Cloud     { get; set; }
        public string ApiKey    { get; set; }
        public string ApiSecret { get; set; }

        private Account Account => new Account(Cloud, ApiKey, ApiSecret);

        public Cloudinary Cloudinary => new Cloudinary(Account);
    }
}