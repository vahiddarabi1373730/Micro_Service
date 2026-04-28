using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products;



public class DeleteProductCommand:IRequest<bool>
{
    public string Id { get; set; }

    public DeleteProductCommand(string id)
    {
        Id = id;
    }
}

public class DeleteProductCommandHandler(IProductRepository repository):IRequestHandler<DeleteProductCommand,bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return  await repository.DeleteProduct(request.Id);
    }
}