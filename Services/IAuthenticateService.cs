using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IAuthenticateService
    {
      string GenerateToken(UserLogin user);

      bool VerifiedPassword(UserLogin user);
    }
}
