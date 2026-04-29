using Catalog.Core.Entities;
using Catalog.Core.Pagination;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastrature.Data;
using MongoDB.Driver;

namespace Catalog.Infrastrature.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository
{
    public async Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams)
    {
        var builder = Builders<Product>.Filter;
        var filters = builder.Empty;
        filters = FilterDefinition(specParams, filters, builder);
        var count = await GetTotalCount(filters);
        SortDefinition<Product> sort;
        DefinitionSort(specParams, out sort);
        var data = await GetData(specParams, filters, sort);
        return new Pagination<Product>(specParams.PageSize, specParams.PageIndex, (int)count, data);
    }

    private async Task<List<Product>> GetData(CatalogSpecParams specParams, FilterDefinition<Product> filters,
        SortDefinition<Product> sort)
    {
        return await context.Products.Find(filters).Sort(sort).Skip(specParams.PageSize * (specParams.PageIndex - 1))
            .Limit(specParams.PageSize).ToListAsync();
    }

    private async Task<long> GetTotalCount(FilterDefinition<Product> filters)
    {
        if (context is null)
        {
            throw new Exception("context are null");
        }

        if (filters is null)
        {
            throw new Exception("Filters are null");
        }

        return await context.Products.CountDocumentsAsync(filters);
    }

    private static void DefinitionSort(CatalogSpecParams specParams, out SortDefinition<Product> sort)
    {
        if (!string.IsNullOrEmpty(specParams.Sort))
        {
            sort = specParams.Sort switch
            {
                "priceAsc" => Builders<Product>.Sort.Ascending(x => x.Price),
                "priceDesc" => Builders<Product>.Sort.Descending(x => x.Price),
                _ => Builders<Product>.Sort.Ascending(x => x.Name)
            };
        }
        else
        {
            sort = Builders<Product>.Sort.Ascending(x => x.Name);
        }
    }

    private static FilterDefinition<Product> FilterDefinition(CatalogSpecParams specParams,
        FilterDefinition<Product> filters,
        FilterDefinitionBuilder<Product> builder)
    {
        if (!string.IsNullOrEmpty(specParams.Search))
        {
            filters &= builder.Where(x => x.Name.ToLower().Contains(specParams.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(specParams.BrandId))
        {
            filters &= builder.Eq(x => x.Brands.Id, specParams.BrandId);
        }

        if (!string.IsNullOrEmpty(specParams.TypeId))
        {
            filters &= builder.Eq(x => x.Types.Id, specParams.TypeId);
        }

        return filters;
    }

    public async Task<Product> GetProductById(string id)
    {
        return await context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await context.Products.Find(x => x.Name == name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeId(string typeId)
    {
        return await context.Products.Find(x => x.Types.Id == typeId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeName(string typeName)
    {
        return await context.Products.Find(x => x.Types.Name == typeName).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandId(string brandId)
    {
        return await context.Products.Find(x => x.Brands.Id == brandId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandName(string brandName)
    {
        return await context.Products.Find(x => x.Brands.Name == brandName).ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var deletedProduct = await context.Products.DeleteOneAsync(x => x.Id == id);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
    }

    public async Task<bool> DeleteProduct(Product product)
    {
        var result = await context.Products.DeleteOneAsync(x => x.Id == product.Id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<bool> DeleteRangeProducts(IEnumerable<Product> products)
    {
        var result = await context.Products.DeleteManyAsync(x => products.Contains(x));
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<Product> AddProduct(Product product)
    {
        await context.Products.InsertOneAsync(product);
        return product;
    }
}