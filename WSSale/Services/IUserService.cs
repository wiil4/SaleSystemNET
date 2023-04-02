using WSSale.Models.Request;
using WSSale.Models.Response;

namespace WSSale.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
