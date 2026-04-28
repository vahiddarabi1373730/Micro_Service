using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastrature.Data;

public class CatalogContext:ICatalogContext
{
    public IMongoCollection<Product> Products { get; set; }
    public IMongoCollection<ProductBrand> ProductBrands { get; set; }
    public IMongoCollection<ProductType> ProductTypes { get; set; }

    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
        var database=client.GetDatabase(configuration.GetValue<string>("DataBaseSettings:DatabaseName"));
        var products=database.GetCollection<Product>(configuration.GetValue<string>("DataBaseSettings:CollectionName"));
        var brands=database.GetCollection<ProductBrand>(configuration.GetValue<string>("DataBaseSettings:BrandsCollection"));
        var types = database.GetCollection<ProductType>(configuration.GetValue<string>("DataBaseSettings:TypesCollection"));
        
        ProductSeadData.SeadData(products);
        ProductBrandSeadData.SeadData(brands);
        ProductTypeSeadData.SeadData(types);
        
        
    }
}