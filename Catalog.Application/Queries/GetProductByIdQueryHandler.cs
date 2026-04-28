using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductByIdQuery:IRequest<ProductResponse>
{
    public string Id { get; set; }

    public GetProductByIdQuery(string id)
    {
        Id = id;
    }
}
public class GetProductByIdQueryHandler(IProductRepository repository,IMapper mapper):IRequestHandler<GetProductByIdQuery,ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetProductById(request.Id);
        if (result is null)
        {
            throw new Exception($"Product is not fount with id ${result.Id}");
        }

        return mapper.Map<ProductResponse>(result);
    }
}