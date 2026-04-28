using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastrature.Data;

public static class ProductTypeSeadData
{
    public static void SeadData(IMongoCollection<ProductType> collection)
    {
        var path=Path.Combine(AppContext.BaseDirectory,"Data","SeadData","types.json");
        if (!File.Exists(path))
        {
            throw new Exception("File not found");
        }

        var dataText = File.ReadAllText(path);
        var brands = JsonSerializer.Deserialize<List<ProductType>>(dataText);
        if (brands is not null)
        {
            collection.InsertMany(brands);
        }
    }
}