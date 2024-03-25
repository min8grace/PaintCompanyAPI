using PaintStockStatusAPI.Models;
using System.Threading.Tasks;

namespace PaintStockStatusAPI.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
