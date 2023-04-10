using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Core.Identity;
using WebWaterPaintStore.Data.Contexts;
using System.Collections.Generic;


namespace WebWaterPaintStore.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {

        private readonly StoreDbContext _dbContext;
        private readonly IPasswordHasher _hasher;

        public DataSeeder(StoreDbContext dbContext, IPasswordHasher hasher)
        {
            _dbContext = dbContext;
            _hasher = hasher;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Products.Any())
            {
                return;
            }

            var categories = AddCategories();
            var products = AddProduct(categories);
            var units = AddUnit(products);
            var orders = AddOrder(products);

            var roles = AddRoles();
            var users = AddUsers(roles);

        }
        private IList<Role> AddRoles()
        {
            var roles = new List<Role>()
        {
            new() {Name = "Admin"},
            new() {Name = "Manager"},
            new() {Name = "Customer"},
        };

            _dbContext.Roles.AddRange(roles);
            _dbContext.SaveChanges();
            return roles;
        }

        private IList<User> AddUsers(IList<Role> roles)
        {
            var users = new List<User>()
        {
            new User()
            {
                Name = "Admin",
                Email = "Admin@gmail.com",
                Address = "DLU",
                Phone = "0123456789",
                Username = "admin",
                Password = _hasher.Hash("admin#123"),
                Roles = new List<Role>()
                {
                    roles[0],
                    roles[1]
                }
            }
        };

            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();

            return users;
        }


        private IList<Category> AddCategories()
        {
            var categories = new List<Category>()
        {
            new() {Name = "Sơn đặt biệt", Description = "Sơn đặt biệt", UrlSlug = "dat-biet"},
            new() {Name = "Sơn ngoại thất", Description = "Sơn ngoại thất", UrlSlug = "ngoai-that"},
            new() {Name = "Sơn nội thất", Description = "Sơn nội thất", UrlSlug = "noi-that"},
            new() {Name = "Sơn dự án", Description = "Sơn dự án", UrlSlug = "du-an"},
            new() {Name = "Sơn chống nóng", Description = "Sơn chống nóng", UrlSlug = "chong-nong"},
        };

            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();
            return categories;
        }


        private IList<Order> AddOrder(IList<Product> products)
        {
            var orders = new List<Order>()
        {
            new ()
            {
                Email = "2014508@gmail.com",
                ShipName = "NT Vũ",
                OrderDate = DateTime.Now,
                ShipAddress = "DLU",
                ShipTel = "012345678",
                Status = OrderStatus.New
            },
            new ()
            {
                Email = "2014508@gmail.com",
                ShipName = "NT Vũ",
                OrderDate = DateTime.Now,
                ShipAddress = "DLU",
                ShipTel = "012345678",
                Status = OrderStatus.New,
                OrderDetails = new List<OrderDetail>()
                {
                    new ()
                    {
                        ProductId = products[0].Id,
                        Quantity = 2,
                        Discount = products[0].UnitDetails[0].Discount
                    },new ()
                    {
                        ProductId = products[1].Id,
                        Quantity = 3,
                        Discount = products[1].UnitDetails[0].Discount
                    },
                }
            }
        };

            _dbContext.Orders.AddRange(orders);
            _dbContext.SaveChanges();
            return orders;
        }




        private IList<Product> AddProduct(IList<Category> categories)
        {
            var product = new List<Product>()
        {
            new ()
            {
                Name = "Sơn giả đá vảy trung - KSP-GOLD",
                ShortDescription = "Sơn giả đá KSP-GOLD là loại sơn được tổng hợp từ các vật liệu bằng đá và các thickners làm đặc là Polyurethane, hình thành liên kết chặt chẽ với dẫn xuất Silicon. Loại này có thể tạo ra nhiều màu sắc, hoa văn gần giống đá thiên nhiên granit, nhưng nhẹ hơn nhiều lần, chịu hóa chất hơn, dễ thi công cho những chi tiết mà không thể ốp được bằng đá tự nhiên.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-gia-da-vay-trung-KSP-GOLD",
                Category = categories[0],
                Actived = true,
                ImageUrl = "",
                CreatedDate = DateTime.Now,
            },
            new ()
            {
                Name = "Sơn bóng cao cấp ngoài trời - K360-GOLD",
                ShortDescription = "K360-Gold là loại sơn phủ cao cấp ngoài trời hệ Latex Acrylonitrile biến tính bằng Epoxy đã được nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. K360-Gold có độ phủ tốt, màng sơn nhẵn, bóng đẹp nên dễ chùi rửa và làm sạch, dùng để sơn phủ sau khi đã làm phẳng tường bằng Mattit KOVA MT-N hoặc Bột bả KOVA MB ngoài trời. Có thể lăn trực tiếp khi tường đã khô và được lót bằng sơn kháng kiềm K209-Gold hoặc một loại sơn trắng ngoài trời của KOVA. K360-Gold chống bám bụi, đặc biệt dưới tác dụng của ánh sáng mặt trời diễn ra quá trình hủy lớp bụi bám vào màng sơn, nên màng sơn luôn mới sau những trận mưa rào.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "son-bongg-cao-cap-ngoai-troi-K360-GOLD",
                Category = categories[1],
                Actived = true,
                ImageUrl = "",
                CreatedDate = DateTime.Now,
            },
            new ()
            {
                Name = "Sơn trang trí, chống thấm cao cấp ngoài trời - CT04T-GOLD",
                ShortDescription = "CT04T-Gold là loại sơn cao cấp phủ ngoài trời hệ Methyl Methacrylat Elastomer, được sử dụng làm chất chống thấm hoặc làm sơn trang trí có độ đàn hồi cao, có khả năng giảm nứt cho màng sơn.",
                Meta = "TCCS 06:2018/KOVA",
                UrlSlug = "son-trang-tri-chong-tham-cao-cap-ngoai-troi-CT04T-GOLD",       
                Category = categories[2],
                Actived = true,
                ImageUrl = "",
                CreatedDate = DateTime.Now,
            },
                new ()
            {
                Name = "Sơn bóng cao cấp trong nhà - K871-GOLD",
                ShortDescription = "K871-Gold là loại sơn hệ Vinyl-latex, phủ trong nhà, chất lượng cao đã hoàn toàn nhiệt đới hóa, rất phù hợp với khí hậu nhiệt đới.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-bong-cao-cap-trong-nha-K871-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "",
                CreatedDate = DateTime.Now,
            },
            new ()
            {
                Name = "Sơn Phủ Ngoại Thất - K68 PRO",
                ShortDescription = "Sơn Phủ Ngoại Thất là loại sơn được tổng hợp từ các vật liệu bằng đá và các thickners làm đặc là Polyurethane, hình thành liên kết chặt chẽ với dẫn xuất Silicon. Loại này có thể tạo ra nhiều màu sắc, hoa văn gần giống đá thiên nhiên granit, nhưng nhẹ hơn nhiều lần, chịu hóa chất hơn, dễ thi công cho những chi tiết mà không thể ốp được bằng đá tự nhiên.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-phu-ngoai-that-K68-PRO",
                Category = categories[3],
                Actived = true,
                ImageUrl = "",
                CreatedDate = DateTime.Now,
            },
            
        };

            _dbContext.Products.AddRange(product);
            _dbContext.SaveChanges();
            return product;
        }

        private IList<UnitDetail> AddUnit(IList<Product> Products)
        {
            var unit = new List<UnitDetail>()
        {
            new() {UnitTag = "4kg", Price = 900000, Quantity = 20, Discount = 0, SoldCount = 22, ProductId = Products[0].Id},
            new() {UnitTag = "20kg", Price = 4500000, Quantity = 4, Discount = 0, SoldCount = 27, ProductId = Products[0].Id},
            new() {UnitTag = "4kg", Price = 1300000, Quantity = 25, Discount = 20, SoldCount = 25, ProductId = Products[1].Id},
            new() {UnitTag = "20kg", Price = 6000000, Quantity = 34, Discount = 0, SoldCount = 12, ProductId = Products[1].Id},
            new() {UnitTag = "4kg", Price = 1000000, Quantity = 40, Discount = 50, SoldCount = 24, ProductId = Products[2].Id},
            new() {UnitTag = "10kg", Price = 2800000, Quantity = 20, Discount = 30, SoldCount = 32, ProductId = Products[2].Id},
            new() {UnitTag = "4kg", Price = 950000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[3].Id},
            new() {UnitTag = "5kg", Price = 400000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[4].Id},
        };

            _dbContext.UnitDetails.AddRange(unit);
            _dbContext.SaveChanges();
            return unit;
        }

    }
}
