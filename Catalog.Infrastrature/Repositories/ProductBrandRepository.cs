using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastrature.Data;
using MongoDB.Driver;

namespace Catalog.Infrastrature.Repositories;

public class ProductBrandRepository(ICatalogContext context):IProductBrandRepository
{
    public async Task<IEnumerable<ProductBrand>> GetProductBrands()
    {
        return await context.ProductBrands.Find(x => true).ToListAsync();
    }
}