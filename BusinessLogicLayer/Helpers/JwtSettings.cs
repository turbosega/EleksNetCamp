using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Helpers
{
    public class JwtSettings
    {
        public string Key      { get; set; }
        public string Issuer   { get; set; }
        public string Audience { get; set; }

        private SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        public  SigningCredentials   SigningCredentials   => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}