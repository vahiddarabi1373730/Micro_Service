using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastrature.Data;
using MongoDB.Driver;

namespace Catalog.Infrastrature.Repositories;

public class ProductTypeRepository(ICatalogContext context):IProductTypeRepository
{
    public async Task<IEnumerable<ProductType>> GetProductTypes()
    {
        return await context.ProductTypes.Find(x => true).ToListAsync();
    }
}