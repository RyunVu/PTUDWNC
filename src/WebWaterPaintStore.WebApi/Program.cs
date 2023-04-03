using TatBlog.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
{
    // use seeder
    app.UseDataSeeder();


    app.Run();
}
