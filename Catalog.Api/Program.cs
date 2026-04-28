using System.Reflection;
using Asp.Versioning;
using Catalog.Core.Repositories;
using Catalog.Infrastrature.Data;
using Catalog.Infrastrature.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Register MediatR
// builder.Services.AddTransient<IRequestHandler<AddProductCommand, ProductResponse>, AddProductCommandHandler>();
// builder.Services.AddTransient<IRequestHandler<deleteProductCommand,bool>,deleteProductCommandHandler>();
//به جای این:
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


//Config Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",new OpenApiInfo(){Description = "Catalog Api",Title = "Catalog"});
});
builder.Services.AddEndpointsApiExplorer();


//Register ApiVersion
builder.Services.AddApiVersioning(option =>
{
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
});



//Di
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository,ProductTypeRepository>();
builder.Services.AddScoped<IProductBrandRepository, ProductBrandRepository>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();