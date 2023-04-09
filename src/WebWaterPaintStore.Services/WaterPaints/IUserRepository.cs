using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Services.WaterPaints
{
    public interface IUserRepository{
        Task<User> GetUser(string userName, string password, CancellationToken cancellationToken = default);

    }
}
