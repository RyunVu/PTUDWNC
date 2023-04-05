using WebWaterPaintStore.WebApi.Endpoints;
using WebWaterPaintStore.WebApi.Extensions;
using WebWaterPaintStore.WebApi.Mapsters;
using WebWaterPaintStore.WebApi.Validations;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .ConfigureCors()
        .ConfigureNLog()
        .ConfigureServices()
        .ConfigureSwaggerOpenApi()
        .ConfigureMapster()
        .ConfigureFluentValidation();
}

var app = builder.Build();
{
    app.UseDataSeeder();
    app.SetupRequestPipeline();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();
    app.UseHttpsRedirection();

    app.UseCors("WebStoreApp");

    app.MapProductEndpoints();


    app.Run();
}
