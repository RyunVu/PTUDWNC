using Microsoft.EntityFrameworkCore;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Core.Identity;
using WebWaterPaintStore.Data.Contexts;

namespace WebWaterPaintStore.Services.WaterPaints
{
    public class UserRepository : IUserRepository{
        private readonly StoreDbContext _dbContext;
        private readonly IPasswordHasher _hasher;

        public UserRepository(StoreDbContext dbcontext, IPasswordHasher hasher)
        {
            _dbContext = dbcontext;
            _hasher = hasher;
        }

        public async Task<User> GetUser(string userName, string password, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Set<User>()
            .Include(s => s.Roles)
            .FirstOrDefaultAsync(user =>
                user.Username.Equals(userName), cancellationToken);

            if (user != null && _hasher.VerifyPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }
    }
}
