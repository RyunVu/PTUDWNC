using WebWaterPaintStore.Data.Contexts;

namespace WebWaterPaintStore.Services.WaterPaints
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _dbContext;

        public OrderRepository(StoreDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
