using Catalog.Core.Entities;
using Catalog.Core.Specs;
using Catalog.Core.Pagination;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
    public Task<Pagination<Product>> GetProducts(CatalogSpecParams  specParams);
    public Task<Product> GetProductById(string id);
    public Task<IEnumerable<Product>> GetProductsByName(string name);
    public Task<IEnumerable<Product>> GetProductsByTypeId(string typeId);
    public Task<IEnumerable<Product>> GetProductsByTypeName(string typeName);
    public Task<IEnumerable<Product>> GetProductsByBrandId(string brandId);
    public Task<IEnumerable<Product>> GetProductsByBrandName(string brandName);
    public Task<Product> AddProduct(Product product);
    public Task<bool> UpdateProduct(Product product);
    public Task<bool> DeleteProduct(string id);
    public Task<bool> DeleteProduct(Product product);
    public Task<bool> DeleteRangeProducts(IEnumerable<Product> products);


}