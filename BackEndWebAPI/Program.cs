using BackEndWebAPI.MiddleLayers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductRepository.Data;
using ProductRepository.ProductRepo.Interface;
using ProductService;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "BackEndWebAPI",
        Version = "v1",
        Description = "An Api for managing product inventory"
    }
    );
   var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
}
);

    builder.Services.AddDbContext<ProductContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConnectionString"));
    });

builder.Services.AddScoped<IProductService, ProductService.ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository.ProductRepo.ProductRepository>();


var app = builder.Build();

// run sql script to create database and table and procedures

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider.GetRequiredService<ProductContext>();
    string sqlScript = File.ReadAllText("..\\ProductRepository\\Data\\dbscript.sql");
    var commands = sqlScript.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
    services.Database.EnsureCreated();
    foreach (var command in commands)
    {
        if (!string.IsNullOrWhiteSpace(command))
        {
            services.Database.ExecuteSqlRaw(command);
        }
    }
}
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
