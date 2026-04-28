using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductTypeRepository
{
    public Task<IEnumerable<ProductType>> GetProductTypes();
}