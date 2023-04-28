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

        public async Task<User> Login(string userName, string password, CancellationToken cancellationToken = default)
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

        public async Task<User> GetUserByIdAsync(int id, bool getFull = false, CancellationToken cancellationToken = default)
        {
            if (getFull)
            {
                return await _dbContext.Set<User>().Include(r=>r.Roles).FirstOrDefaultAsync(r => r.Id == id,cancellationToken);
            }
            return await _dbContext.Set<User>().FirstOrDefaultAsync(r =>r.Id == id,cancellationToken);
        }

        public async Task<User> Register(User user, IEnumerable<int> roles, CancellationToken cancellationToken = default)
        {
            var userExist = await _dbContext.Set<User>().AnyAsync(u => u.Username.Equals(user.Username), cancellationToken);
            if (userExist)
            {
                return null;
            }
            user.Roles = new List<Role>();
            user.Password = _hasher.Hash(user.Password);

            if(UpdateUserRole(user, roles))
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return user;
            }

            return null;
        }

        public async Task<Role> GetRoleByName(string role, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Role>()
                .Include(u => u.Users)
                .FirstOrDefaultAsync(u => u.Name.Equals(role), cancellationToken);
        }

        public async Task<bool> IsUserExistedAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>().AnyAsync(u => u.Username.Equals(userName), cancellationToken);
        }

        public bool UpdateUserRole(User user, IEnumerable<int> selectRoles)
        {
            if (selectRoles == null) return false;

            var roles = _dbContext.Roles.ToList();
            var currenetRoleNames = new HashSet<int>(user.Roles.Select(x => x.Id));

            foreach ( var role in roles) {
                if (selectRoles.ToList().Contains(role.Id))
                {
                    if(!currenetRoleNames.ToList().Contains(role.Id))
                    {
                        user.Roles.Add(role);
                    }
                }
                else if (currenetRoleNames.ToList().Contains(role.Id))
                {
                    user.Roles.Remove(role);
                }
            }
            return true;
        }

    }
}
