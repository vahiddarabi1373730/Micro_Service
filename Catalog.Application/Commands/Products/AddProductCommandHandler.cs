using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products;


public class AddProductCommand:IRequest<ProductResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public decimal Price { get; set; }
    public string ImageFile { get; set; }
    public ProductType Types { get; set; }
    public ProductBrand Brands {get; set;}
    
}


public class AddProductCommandHandler(IProductRepository repository,IMapper mapper):IRequestHandler<AddProductCommand,ProductResponse>
{
    public async Task<ProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Product>(request);
        var result = await repository.AddProduct(entity);
        return mapper.Map<ProductResponse>(result);
    }
}