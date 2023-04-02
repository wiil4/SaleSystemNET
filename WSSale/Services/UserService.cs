using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WSSale.Models;
using WSSale.Models.Common;
using WSSale.Models.Request;
using WSSale.Models.Response;
using WSSale.Tools;

namespace WSSale.Services
{
    public class UserService : IUserService
    {
        private readonly RealSaleContext _realSaleContext;
        private readonly AppSettings _appSettings;

        public UserService(RealSaleContext realSaleContext, IOptions<AppSettings> appSettings)
        {
            _realSaleContext = realSaleContext;
            _appSettings = appSettings.Value;
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse uResponse = new UserResponse();
            string password = Encrypt.GetSha256(model.Password);
            var user = _realSaleContext.Users.Where(d => d.Email == model.Email && d.Password == password).FirstOrDefault();

            if (user == null) return null;

            uResponse.Email = user.Email;
            uResponse.Token = GetToken(user);
            return uResponse;            
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
