using System.Threading.Tasks;

namespace ConferencePlanner.Services
{
    public interface IAdminService
    {
        Task<bool> AllowAdminUserCreationAsync();
    }
}