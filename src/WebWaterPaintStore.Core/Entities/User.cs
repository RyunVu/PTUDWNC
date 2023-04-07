using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public IList<Role> Roles { get; set; }
    }

    public class Role : IEntity {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<User> Users { get; set; }
    }
}
