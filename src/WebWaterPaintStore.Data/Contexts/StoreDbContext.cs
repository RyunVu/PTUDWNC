using Microsoft.EntityFrameworkCore;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Data.Mappings;

namespace WebWaterPaintStore.Data.Contexts
{
    public class StoreDbContext : DbContext{

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UnitDetail> UnitDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-7NPFO5S;Database=WaterPaintStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryMap).Assembly);
        }
    }
}
