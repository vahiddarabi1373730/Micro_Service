using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastrature.Data;

public static class ProductBrandSeadData
{
    public static void SeadData(IMongoCollection<ProductBrand> collection)
    {
        var path=Path.Combine(AppContext.BaseDirectory,"Data","SeadData","brands.json");
        if (!File.Exists(path))
        {
            throw new Exception("File not found");
        }

        var dataText = File.ReadAllText(path);
        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(dataText);
        if (brands is not null)
        {
            collection.InsertMany(brands);
        }
    }
}