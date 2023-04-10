using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Services.WaterPaints
{
    public interface IUserRepository{
        Task<User> Login(string userName, string password, CancellationToken cancellationToken = default);

        Task<User> GetUserByIdAsync(int id, bool getFull = false, CancellationToken cancellationToken = default);
        
        Task<User> Register(User user, IEnumerable<int> roles, CancellationToken cancellationToken = default);

        Task<Role> GetRoleByName(string role, CancellationToken cancellationToken = default);

        Task<bool> IsUserExistedAsync(string userName, CancellationToken cancellationToken = default);
    }
}
