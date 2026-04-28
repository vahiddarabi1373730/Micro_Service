using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductBrandRepository
{
    public Task<IEnumerable<ProductBrand>> GetProductBrands();
}