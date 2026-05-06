using System.Reflection;
using Asp.Versioning;
using Basket.Application.External;
using Basket.Application.Mapper;
using Basket.Application.Queries;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Discount.Application.Protos.discount.proto;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);


//////////////////Config Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",new OpenApiInfo(){Description = "Basket Api",Title = "Basket"});
});
builder.Services.AddEndpointsApiExplorer();

///////////////////////Config AutoMapper
builder.Services.AddAutoMapper(typeof(ProfileMapper));


/////////////////////////////////////ConfigMediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetBasketQueryHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));


//////////////////////////////////////////Config ApiVersioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1,0);
    
});


/////////////////////////////////////DI
builder.Services.AddScoped<IBasketRepositories, BasketRepositories>();
builder.Services.AddScoped<DiscountGrpcService>();


////////////////////////////////////////Config Grpc
builder.Services.AddGrpcClient<DiscountProtoServices.DiscountProtoServicesClient>(options =>
{
    options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")!);
});


///////////////////////////////////////Config Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
