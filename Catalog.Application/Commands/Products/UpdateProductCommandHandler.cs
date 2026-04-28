using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products;



public class UpdateProductCommand:IRequest<bool>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public decimal Price { get; set; }
    public string ImageFile { get; set; }
    public ProductType Types { get; set; }
    public ProductBrand Brands {get; set;}
}

public class UpdateProductCommandHandler(IProductRepository repository,IMapper mapper):IRequestHandler<UpdateProductCommand,bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Product>(request);
        return await repository.UpdateProduct(entity);
    }
}