using System.Reflection;
using Asp.Versioning;
using AutoMapper;
using Discount.Api.Services;
using Discount.Application.Mapper;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Infrastructure.Extension;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

///////////////////////Config AutoMapper
builder.Services.AddAutoMapper(typeof(ProfileMapper));


/////////////////////////////////////ConfigMediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetDiscountByProductIdQueryHandler).Assembly
};
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies));


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(9003, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    } );
    options.ListenLocalhost(9002, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    } );
});


//////////////////////////////////////////Config ApiVersioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1,0);
    
});


/////////////////////////////////////DI
builder.Services.AddScoped<IDiscountRepositories,DiscountRepositories>();


////////////////////Grpc
builder.Services.AddGrpc();


var app = builder.Build();

app.MigrateDatabase<Profile>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


//////////////////////Grpc
app.MapGrpcService<DiscountService>();

app.MapControllers();
app.Run();

