using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastrature.Data;

public static class ProductSeadData
{
    public static void SeadData(IMongoCollection<Product> collection)
    {
        var existCollection = collection.Find(x => true).Any();
        if (existCollection) return;
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "SeadData", "products.json");
        if (!File.Exists(path))
        {
            throw new Exception("File not found");
        }

        var dataText = File.ReadAllText(path);
        var products = JsonSerializer.Deserialize<List<Product>>(dataText);
        if (products is not null)
        {
            collection.InsertMany(products);
        }
    }
}