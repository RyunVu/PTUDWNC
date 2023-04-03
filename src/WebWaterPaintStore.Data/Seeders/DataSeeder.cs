using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWaterPaintStore.Data.Contexts;

namespace WebWaterPaintStore.Data.Seeders
{
    public class DataSeeder : IDataSeeder{

        private readonly ShopDbContext _dbContext;

        public DataSeeder(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize(){

        }
    }
}
