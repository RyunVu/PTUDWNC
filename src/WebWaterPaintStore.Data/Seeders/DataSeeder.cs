using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Core.Identity;
using WebWaterPaintStore.Data.Contexts;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;


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
            //var orders = AddOrder(products);

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
            new() {Name = "Sơn chống thấm", Description = "Sơn chống thấm", UrlSlug = "chong-tham"},
            new() {Name = "Sơn ngoại thất", Description = "Sơn ngoại thất", UrlSlug = "ngoai-that"},
            new() {Name = "Sơn nội thất", Description = "Sơn nội thất", UrlSlug = "noi-that"},
            new() {Name = "Sơn dự án", Description = "Sơn dự án", UrlSlug = "du-an"},
            new() {Name = "Sơn sàn thể thao, sàn công nghiệp", Description = "Sơn sàn thể thao, sàn công nghiệp", UrlSlug = "san-the-thao-cong-nghiep"},
            new() {Name = "Sơn chống nóng", Description = "Sơn chống nóng", UrlSlug = "chong-nong"},
            new() {Name = "Sơn phủ bóng không màu trong suốt", Description = "Sơn phủ bóng không màu trong suốt", UrlSlug = "phu-bong"},
            new() {Name = "Sơn nhũ", Description = "Sơn nhũ", UrlSlug = "nhu"},
            new() {Name = "Matit", Description = "Matit", UrlSlug = "matit"},
        };

            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();
            return categories;
        }


        //private IList<Order> AddOrder(IList<Product> products)
        //{
        //    var orders = new List<Order>()
        //{
        //    new ()
        //    {
        //        Email = "2014508@gmail.com",
        //        ShipName = "NT Vũ",
        //        OrderDate = DateTime.Now,
        //        ShipAddress = "DLU",
        //        ShipTel = "012345678",
        //        Status = OrderStatus.New
        //    },
        //    new ()
        //    {
        //        Email = "2014508@gmail.com",
        //        ShipName = "NT Vũ",
        //        OrderDate = DateTime.Now,
        //        ShipAddress = "DLU",
        //        ShipTel = "012345678",
        //        Status = OrderStatus.New,
        //        //OrderDetails = new List<OrderDetail>()
        //        //{
        //        //    new ()
        //        //    {
        //        //        ProductId = products[0].Id,
        //        //        Quantity = 2,
        //        //        Discount = products[0].UnitDetails[0].Discount
        //        //    },new ()
        //        //    {
        //        //        ProductId = products[1].Id,
        //        //        Quantity = 3,
        //        //        Discount = products[1].UnitDetails[0].Discount
        //        //    },
        //        //}
        //    }
        //};

        //    _dbContext.Orders.AddRange(orders);
        //    _dbContext.SaveChanges();
        //    return orders;
        //}




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
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s400_400/ksp-v-1658471060.jpg",
                CreatedDate = DateTime.Now,
            },


             new ()
            {
                Name = "Sơn kẻ đường cho bê tông nhựa, bê tông xi măng, sơn tấm chắn con lươn - K462",
                ShortDescription = "Là loại sơn giao thông hệ Cao su Clo hóa, có thể sơn trực tiếp lên bề mặt xi măng, bê tông, nhựa, bám dính tốt trên bề mặt thép (không áp dụng cho nhựa mềm, tôn tráng kẽm hoặc nhôm)",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-ke-duong-cho-be-tong-k462",
                Category = categories[0],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k462v-1658475216.jpg",
                CreatedDate = DateTime.Now,
            },


            new ()
            {
                Name = "Chất chống thấm xi măng, bê tông - CT-11A GOLD",
                ShortDescription = "Chất chống thấm được tổng hợp từ Acrylonitrile và Alkylsiloxan. Có tác dụng ngăn sự thấm nước từ ngoài vào, nhưng bề mặt vẫn bôc hơi nước dễ dàng. Là sản phẩm lỏng hệ nước, được dùng để chống thấm cho xi măng, bê tông dưới dạng hỗn hợp với xi măng theo tỉ lệ 1:1. Chất chống thấm thích hợp cho bê tông, nền, tường xi măng như: đường hầm, bể nước, bể bơi, sân thượng, nền nhà, bờ tường, sê nô, sàn vệ sinh... Độ bền vững thử nghiệm ở các tòa nhà cao tầng tại Mỹ trên 15 năm, chất lượng vẫn tốt.",
                Meta = "2.0-2.5 m2/kg, tùy thuộc bề mặt vật liệu.",
                UrlSlug = "chat-chong-tham-xi-mang-be-tong-ct-11a-gold",
                Category = categories[1],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/ct-11av-1658471439.jpg",
                CreatedDate = DateTime.Now,
            },
              new ()
            {
                Name = "Phụ gia trộn vữa xi măng, bê tông - CT-11B GOLD",
                ShortDescription = "Chất chống thấm được tổng hợp từ Acrylonitrile và Alkylsiloxan. Có tác dụng ngăn sự thấm nước từ ngoài vào, nhưng bề mặt vẫn bôc hơi nước dễ dàng. Là sản phẩm lỏng hệ nước, được dùng để chống thấm cho xi măng, bê tông dưới dạng hỗn hợp với xi măng theo tỉ lệ 1:1. Chất chống thấm thích hợp cho bê tông, nền, tường xi măng như: đường hầm, bể nước, bể bơi, sân thượng, nền nhà, bờ tường, sê nô, sàn vệ sinh... Độ bền vững thử nghiệm ở các tòa nhà cao tầng tại Mỹ trên 15 năm, chất lượng vẫn tốt.",
                Meta = "2.0-2.5 m2/kg, tùy thuộc bề mặt vật liệu.",
                UrlSlug = "phu-tron-ct-11b-gold",
                Category = categories[1],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/ct11b-v-1658475302.jpg",
                CreatedDate = DateTime.Now,
            },
                new ()
            {
                Name = "Chất chống thấm co giãn, chống áp lực ngược cho xi măng, bê tông - CT-14 GOLD",
                ShortDescription = "Chất chống thấm được tổng hợp từ Acrylonitrile và Alkylsiloxan. Có tác dụng ngăn sự thấm nước từ ngoài vào, nhưng bề mặt vẫn bôc hơi nước dễ dàng. Là sản phẩm lỏng hệ nước, được dùng để chống thấm cho xi măng, bê tông dưới dạng hỗn hợp với xi măng theo tỉ lệ 1:1. Chất chống thấm thích hợp cho bê tông, nền, tường xi măng như: đường hầm, bể nước, bể bơi, sân thượng, nền nhà, bờ tường, sê nô, sàn vệ sinh... Độ bền vững thử nghiệm ở các tòa nhà cao tầng tại Mỹ trên 15 năm, chất lượng vẫn tốt.",
                Meta = "2.0-2.5 m2/kg, tùy thuộc bề mặt vật liệu.",
                UrlSlug = "chat-chong-tham-co-gian-ap-luc-ct-14-gold",
                Category = categories[1],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/ct-14-v-1658475387.jpg",
                CreatedDate = DateTime.Now,
            },



            new ()
            {
                Name = "Sơn bóng cao cấp ngoài trời - K360-GOLD",
                ShortDescription = "K360-Gold là loại sơn phủ cao cấp ngoài trời hệ Latex Acrylonitrile biến tính bằng Epoxy đã được nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. K360-Gold có độ phủ tốt, màng sơn nhẵn, bóng đẹp nên dễ chùi rửa và làm sạch, dùng để sơn phủ sau khi đã làm phẳng tường bằng Mattit KOVA MT-N hoặc Bột bả KOVA MB ngoài trời. Có thể lăn trực tiếp khi tường đã khô và được lót bằng sơn kháng kiềm K209-Gold hoặc một loại sơn trắng ngoài trời của KOVA. K360-Gold chống bám bụi, đặc biệt dưới tác dụng của ánh sáng mặt trời diễn ra quá trình hủy lớp bụi bám vào màng sơn, nên màng sơn luôn mới sau những trận mưa rào.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "son-bong-cao-cap-ngoai-troi-K360-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k360-v-1658472653.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn bán bóng cao cấp ngoài trời - K5800-GOLD",
                ShortDescription = "K5800-Gold: Là loại sơn phủ bán bóng ngoài trời chất lượng cao, đã được nhiệt đới hóa hoàn toàn rất phù hợp với khí hậu nhiệt đới. Dùng để phủ ngoài trời sau khi đã làm phẳng tường bằng Matit KOVA và sơn lót kháng kiềm K209-Gold..",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "son-ban-bong-ngoai-troi-K5800-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k5800-mt-1658068718.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn không bóng cao cấp ngoài trời - K5501-GOLD",
                ShortDescription = "K5501-Gold: là loại sơn phủ ngoài trời chất lượng cao, đã nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. Dùng để sơn phủ, sau khi làm phẳng tường bằng Matit KOVA MT-N hoặc Bột bả KOVA MB ngoài trời. Có thể lăn trực tiếp khi tường đã khô và được lót bằng sơn kháng kiềm K209-Gold hoặc một loại sơn trắng ngoài trời của KOVA.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "son-khong-bong-cao-cap-ngoai-troi-K5501-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k5501-v-1658472808.jpg",
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
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s400_400/ct04-1639718546.jpg",
                CreatedDate = DateTime.Now,
            },



            new ()
            {
                Name = "Sơn lót kháng kiềm cao cấp ngoài trời - K209-GOLD",
                ShortDescription = "MÔ TẢ K209-Gold là sơn phủ ngoài trời, hệ nước trên cơ sở Methyl Methacrylat có tính kháng kiềm cao, chịu được thời tiết hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. K209-Gold có tác dụng kháng kiềm cao đối với vôi tự do có mặt trong xi măng, có tính chống thấm cho bề mặt bê tông xi măng, phù hợp với mọi điều kiện thời tiết khí hậu ẩm ướt ở vùng nhiệt đới gió mùa, hơi nước mặn. K209-Gold được dùng làm sơn lót ngoài trời trước khi phủ các loại sơn hoàn thiện như K5501-Gold, K360-Gold, CT04T-Gold,... áp dụng cho tất cả các bề mặt bên ngoài của tường xây bê tông, xi măng. Cũng có thể pha màu để làm sơn hoàn thiện như các sơn màu khác. K209-Gold có độ phủ cao, bền, kho nhanh, dễ thi công, bề mặt sơn mịn màng, liên kết chặt chẽ với xi măng và sơn phủ hoàn thiện ngoài trời. K209-Gold không cháy, không độc hại, không chứa chì, thủy ngân và các hóa chất độc hại khác, an toàn cho người thi công và sử dụng Định mức 5.0-5.5 m2/kg (1 lớp) tùy theo bề mặt vật liệu. Vệ sinh dụng cụ: dùng xà bông và nước sạch rửa bình thường. Bảo quản: Để nơi khô mát, đậy kín khi dùng không hết (nếu chưa pha nước). Thời gian lưu kho: 1 năm trong điều kiện khô, thoáng mát và chưa mở nắp. Đóng gói: 4kg và 20kg/ thùng Thành phần chính: trên cơ sở chất tạo màng Metyl Methacrylal, Titan Oxit, các chất phụ gia hoạt tính và bổ trợ.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "son-lot-khang-kiem-ngoai-troi-K209-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k209-v-1658472483.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn màu pha sẵn ngoài trời màu đậm - K280-GOLD",
                ShortDescription = "K280-Gold: là loại sơn hệ Acrylic 100% phủ ngoài trời, đã nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. Dùng để sơn phủ, sau khi làm phẳng tường bằng Matit KOVA MTN-Gold hoặc Bột bả KOVA MB-N. Có thể lăn trực tiếp khi tường đã khô và được lót bằng một loại sơn trắng ngoài trời của KOVA.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "mau-pha-san-ngoai-troi-K280-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k280-1639720668.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn không bóng ngoài trời - K261-GOLD",
                ShortDescription = " K261-Gold: là loại sơn hệ Acrylic 100% đã nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. Dùng để sơn phủ, sau khi làm phẳng tường bằng Matit KOVA MTN-Gold hoặc Bột bả KOVA MB-N. Có thể lăn trực tiếp khi tường đã khô và được lót bằng một loại sơn trắng ngoài trời của KOVA.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "son-khong-bong-ngoai-troi-K261-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k261-1654583954.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Matit ngoài trời - MTN-GOLD",
                ShortDescription = "K360-Gold là loại sơn phủ cao cấp ngoài trời hệ Latex Acrylonitrile biến tính bằng Epoxy đã được nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. K360-Gold có độ phủ tốt, màng sơn nhẵn, bóng đẹp nên dễ chùi rửa và làm sạch, dùng để sơn phủ sau khi đã làm phẳng tường bằng Mattit KOVA MT-N hoặc Bột bả KOVA MB ngoài trời. Có thể lăn trực tiếp khi tường đã khô và được lót bằng sơn kháng kiềm K209-Gold hoặc một loại sơn trắng ngoài trời của KOVA. K360-Gold chống bám bụi, đặc biệt dưới tác dụng của ánh sáng mặt trời diễn ra quá trình hủy lớp bụi bám vào màng sơn, nên màng sơn luôn mới sau những trận mưa rào.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "matit-ngoai-troi-MTN-GOLD",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/mtn-v-1658471648.jpg",
                CreatedDate = DateTime.Now,
            },

                new ()
            {
                Name = "Bột bả ngoài trời - MB-N",
                ShortDescription = "MB-N là sản phẩm được chế tạo từ Polymer Acrylic Styren và các Tamol điển hình, có khả năng biến tính xi măng, làm tăng khả năng bám dính, chống rạn nứt xi măng, đặc biệt làm phẳng bề mặt trước khi sơn các loại sơn bóng và không bóng.",
                Meta = "Tất cả các bề mặt phải khô, được làm sạch bụi, dầu mỡ, vôi và các lớp rêu mốc. Nếu có vết nứt nhỏ cần được hàn gắn bằng 3 lớp CT11A-Gold, nếu lớp thì trám trét bằng vữa CT11B-Gold",
                UrlSlug = "bot-ba-ngoai-troi-MB-N",
                Category = categories[2],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/bbnt-1678936902.jpg",
                CreatedDate = DateTime.Now,
            },




                new ()
            {
                Name = "Sơn bóng cao cấp trong nhà - K871-GOLD",
                ShortDescription = "K871-Gold là loại sơn hệ Vinyl-latex, phủ trong nhà, chất lượng cao đã hoàn toàn nhiệt đới hóa, rất phù hợp với khí hậu nhiệt đới.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-bong-cao-cap-trong-nha-K871-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s400_400/k871-v-1658472987.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn bán bóng cao cấp trong nhà - K5500-GOLD",
                ShortDescription = "ATrang chủ Sơn nội thất. Sơn bán bóng cao cấp trong nhà Mã số sản phẩm:K5500-GOLD. Sơn bán bóng cao cấp trong nhà. Giá:3.458.000 VND. Khối lượng:. Tài liệu kỹ thuật: Thông tin chung Thông số kỹ thuật Hướng dẫn thi công. K5500-Gold: là loại sơn hệ nước Acrylic phủ trong nhà, đã hoàn toàn nhiệt đới hóa, rất phù hợp với khí hậu nhiệt đới. Dùng để sơn phủ, sau khi làm phẳng tường bằng Matit KOVA MTT-Gold hoặc Bột bả KOVA MB-T. Có thể lăn trực tiếp khi tường đã khô và được lót bằng sơn lót kháng kiềm hoặc một loại sơn trắng trong nhà của KOVA.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-ban-bong-cao-cap-trong-nha-K5500-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k5500-v-1658473126.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn lót kháng kiềm cao cấp trong nhà - K109-GOLD",
                ShortDescription = "K109-Gold là loại sơn hệ nước trên cơ sở Methyl Methacrylat, được dùng làm sơn lót kháng kiềm trong nhà, có thể dùng cho ngoài trời. K109-Gold có tác dụng kháng kiềm cao đối với vôi tự do có mặt trong xi măng, có tính chống thấm cho bề mặt bê tông, xi măng, phù hợp với mọi thời tiết khí hậu ẩm ướt ở vùng nhiệt đới gió mùa, hơi nước mặn,...",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-lot-khang-kiem-trong-nha-K109-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k109-v-1658472345.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn trắng trần trong nhà - K10-GOLD",
                ShortDescription = "K10-Gold: Là loại sơn hệ Acrylic, phủ trong nhà có chất lượng cao, đã nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. Dùng để phủ trong nhà sau khi đã làm phẳng tường bằng Matit KOVA hoặc bột bả KOVA trong nhà hoặc có thể lăn trực tiếp lên bề mặt tường đã khô.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-trang-tran-trong-nha-K10-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k10-v-1658473276.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn không bóng trong nhà - K771-GOLD",
                ShortDescription = "K771-Gold là loại sơn hệ Acrylic phủ trong nhà, đã hoàn toàn nhiệt đới hóa, rất phù hợp với khí hậu nhiệt đới. Dùng để sơn phủ, sau khi làm phẳng tường bằng Mattit KOVA MT-T hoặc Bột bả KOVA MB trong nhà. Có thể lăn trực tiếp khi tường đã khô để làm sơn lót hoặc làm sơn màu, sau khi được lót bằng một loại sơn trắng trong nhà của KOVA.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-khong-bong-trong-nha-K771-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k771-v-1658473435.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn không bóng trong nhà - K260-GOLD",
                ShortDescription = "K260-Gold: là loại sơn hệ Acrylic 100% phủ trong nhà, đã hoàn toàn nhiệt đới hóa, phù hợp với khí hậu nhiệt đới. Dùng để sơn phủ, sau khi làm phẳng tường bằng Matit KOVA MTT-Gold hoặc Bột bả KOVA MB-T. Có thể lăn trực tiếp khi tường đã khô và được lót bằng một loại sơn trắng ngoài trời của KOVA.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-khong-bong-trong-nha-K260-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k260-v-1658473602.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn màu pha sẵn trong nhà - K180-GOLD",
                ShortDescription = "K180-Gold: là loại sơn hệ Acrylic phủ trong nhà, đã nhiệt đới hóa hoàn toàn, rất phù hợp với khí hậu nhiệt đới. Dùng để sơn phủ, sau khi làm phẳng tường bằng Matit KOVA MTT-Gold hoặc Bột bả KOVA MB-T. Có thể lăn trực tiếp khi tường đã khô và được lót bằng một loại sơn trắng trong nhà của KOVA.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-mau-pha-san-trong-nha-K180-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k180-v-1658473768.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Matit trong nhà - MTT-GOLD",
                ShortDescription = "MATIT KOVA: Là loại Matit thông dụng để làm phẳng tường nhà trong nhà (MTT-Gold) và ngoài trời (MTN-Gold), đã được nhiệt đới hóa hoàn toàn, sử dụng bả lót nhằm làm phẳng tường trước khi phủ sơn.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "matit-trong-nha-MTT-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/mtt-v-1658473852.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Bột bả trong nhà - MB-T",
                ShortDescription = "MB-T là sản phẩm được chế tạo từ Polumer Acrylic Styren và các Tamol điển hình, có khả năng biến tính xi măng, làm tăng khả năng bám dính, chống rạn nứt xi măng, đặc biệt làm phẳng bề mặt trước khi sơn các loại sơn bóng và không bóng.",
                Meta = "TCCS 08:2018/KOVA",
                UrlSlug = "son-lot-khang-kiem-trong-nha-K109-GOLD",
                Category = categories[3],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/bbtn-1678936953.jpg",
                CreatedDate = DateTime.Now,
            },




            new ()
            {
                Name = "Sơn Phủ Ngoại Thất - K68 PRO",
                ShortDescription = "Sơn Phủ Ngoại Thất là loại sơn được tổng hợp từ các vật liệu bằng đá và các thickners làm đặc là Polyurethane, hình thành liên kết chặt chẽ với dẫn xuất Silicon. Loại này có thể tạo ra nhiều màu sắc, hoa văn gần giống đá thiên nhiên granit, nhưng nhẹ hơn nhiều lần, chịu hóa chất hơn, dễ thi công cho những chi tiết mà không thể ốp được bằng đá tự nhiên.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-phu-ngoai-that-K68-PRO",
                Category = categories[4],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s400_400/k68pro-1658068615.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn Phủ Nội Thất - K58 PRO",
                ShortDescription = "Sơn Phủ Nội Thất là loại sơn được tổng hợp từ các vật liệu bằng đá và các thickners làm đặc là Polyurethane, hình thành liên kết chặt chẽ với dẫn xuất Silicon. Loại này có thể tạo ra nhiều màu sắc, hoa văn gần giống đá thiên nhiên granit, nhưng nhẹ hơn nhiều lần, chịu hóa chất hơn, dễ thi công cho những chi tiết mà không thể ốp được bằng đá tự nhiên.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-phu-noi-that-K58-PRO",
                Category = categories[4],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k58-pro-1658068566.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn Lót kháng kiềm Ngoại Thất - K28 PRO",
                ShortDescription = "Là loại sơn được tổng hợp từ các vật liệu bằng đá và các thickners làm đặc là Polyurethane, hình thành liên kết chặt chẽ với dẫn xuất Silicon. Loại này có thể tạo ra nhiều màu sắc, hoa văn gần giống đá thiên nhiên granit, nhưng nhẹ hơn nhiều lần, chịu hóa chất hơn, dễ thi công cho những chi tiết mà không thể ốp được bằng đá tự nhiên.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-lot-khang-kiem-ngoai-that-K28-PRO",
                Category = categories[4],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k28pro-1658068484.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn Lót kháng kiềm nội thất - K18 PRO",
                ShortDescription = "Là loại sơn được tổng hợp từ các vật liệu bằng đá và các thickners làm đặc là Polyurethane, hình thành liên kết chặt chẽ với dẫn xuất Silicon. Loại này có thể tạo ra nhiều màu sắc, hoa văn gần giống đá thiên nhiên granit, nhưng nhẹ hơn nhiều lần, chịu hóa chất hơn, dễ thi công cho những chi tiết mà không thể ốp được bằng đá tự nhiên.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-phu-ngoai-that-K68-PRO",
                Category = categories[4],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/k18pro-1658068275.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Matit KL5T Aqua GOLD - MT KL5T AQUA - GOLD",
                ShortDescription = "Matit KL5-Aqua GOLD gồm ba thành phần: Bột Aqua, Sơn và phụ gia.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "matit-MT-KL5T-AQUA-GOLD",
                Category = categories[5],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/mt-kl5t-aqua-v-1678765368.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn lót chịu mài mòn - KL5T-GOLD",
                ShortDescription = "gồm hai thành phần: Sơn và phụ gia.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-lot-chiu-mai-mon-KL5T-GOLD",
                Category = categories[5],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/kl5t-v-1658471824.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn sân Tennis, sàn thể thao đa năng màu - CT08-GOLD",
                ShortDescription = "CT08-Gold: là lớp phủ đặc biệt, có độ bền cao, có tính dẻo dai, độ đàn hồi tuyệt vời. Được dùng để phủ các bề mặt bê tông, kim loại, gỗ, nhựa, đường, vật liệu Composit... làm sân thể thao (tennis, cầu lông), đường chạy điền kinh, đường trong công viên, biệt thự, hành lang trong nhà và ngoài trời, sân thượng, vườn cây cảnh.",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-san-tenis-the-thao-da-nang-CT08-GOLD",
                Category = categories[5],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/ct08-v-1658471994.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Sơn chóng nóng - CN-05",
                ShortDescription = "Là sơn chóng nóng",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-chong-nong-CN-05",
                Category = categories[6],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/cn05-1658475473.jpg",
                CreatedDate = DateTime.Now,
            },


            new ()
            {
                Name = "Sơn phủ bóng Clear ngoài trời - CLEAR N - GOLD",
                ShortDescription = "Là sơn phủ bóng ngoài trời",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-phu-bong-clear-ngoai-troi-CLEAR-N-GOLD",
                Category = categories[7],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/clearnv-1658462021.jpg",
                CreatedDate = DateTime.Now,
            },

             new ()
            {
                Name = "Sơn nhũ vàng chùa Thái lan - NT26",
                ShortDescription = "Là sơn nhũ vàng",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "son-nhu-vang-NT26",
                Category = categories[8],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/nt26v-1658474821.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Matit KL5T hai thành phần chịu mài mòn (loại mịn) - MT KL5T - GOLD MỊN",
                ShortDescription = "Matit KL5T mịn",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "matit-KL5T-GOLD-Min",
                Category = categories[9],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/mt-kl5t-v-1658464573.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Matit KL5T hai thành phần chịu mài mòn (loại thô) - MT KL5T - GOLD THÔ",
                ShortDescription = "Matit KL5T thô",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "matit-KL5T-GOLD-Tho",
                Category = categories[9],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/mt-kl5t-v-1658464589.jpg",
                CreatedDate = DateTime.Now,
            },

            new ()
            {
                Name = "Chất phủ đệm sân Thể thao, sân Tennis - TNA - GOLD",
                ShortDescription = "TNA",
                Meta = "TCCS 30:2018/KOVA",
                UrlSlug = "chat-phu-dem-san-the-thao-tenis-TNA-GOLD",
                Category = categories[9],
                Actived = true,
                ImageUrl = "http://kovapaint.com.vn/mediacenter/media/images/3005/products/3005/3596/s1000_1000/tna-v-1658464179.jpg",
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
            new() {UnitTag = "4kg", Price = 900000, Quantity = 20, Discount = 0, SoldCount = 22, ProductId = Products[0].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 4500000, Quantity = 4, Discount = 0, SoldCount = 27, ProductId = Products[0].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 600000, Quantity = 25, Discount = 20, SoldCount = 25, ProductId = Products[1].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 840000, Quantity = 25, Discount = 20, SoldCount = 25, ProductId = Products[2].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 3955000, Quantity = 34, Discount = 0, SoldCount = 12, ProductId = Products[2].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 555000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[3].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2459000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[3].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 785000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[4].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 3745000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[4].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 1250000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[5].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 5925000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[5].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 860000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[6].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 4170000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[6].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 659000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[7].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 3035000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[7].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 986000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[8].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 4648000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[8].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 555000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[9].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2459000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[9].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 795000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[10].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 3628000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[10].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 505000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[11].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2332000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[11].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 428000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[12].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1936000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[12].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 650000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[13].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 948000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[14].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 5000000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[14].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 948000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[15].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 44668000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[15].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 726000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[16].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 3458000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[16].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 480000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[17].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2208000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[17].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 495000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[18].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2295000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[18].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 292000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[19].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1235000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[19].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 330000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[20].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1489000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[20].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 279000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[21].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1219000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[21].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 750000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[22].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 425000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[23].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1740000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[23].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 272000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[24].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1188000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[24].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 272000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[25].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1188000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[25].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 285000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[26].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1200000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[26].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 560000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[27].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2626000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[27].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 1360000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[28].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 6589000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[28].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 1459000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[29].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 6990000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[29].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 650000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[30].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 3480000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[30].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 1000000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[31].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 5175000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[31].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 668000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[32].Id, Actived = true},

            new() {UnitTag = "4kg", Price = 605000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[33].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2295000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[33].Id, Actived = true},
            new() {UnitTag = "4kg", Price = 560000, Quantity = 30, Discount = 20, SoldCount = 14, ProductId = Products[34].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 2135000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[34].Id, Actived = true},
            new() {UnitTag = "20kg", Price = 1478000, Quantity = 20, Discount = 0, SoldCount = 2, ProductId = Products[35].Id, Actived = true},
        };

            _dbContext.UnitDetails.AddRange(unit);
            _dbContext.SaveChanges();
            return unit;
        }

    }
}
