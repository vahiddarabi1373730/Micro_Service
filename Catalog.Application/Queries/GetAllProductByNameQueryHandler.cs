using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductByNameQuery : IRequest<IEnumerable<ProductResponse>>
{
    public string Name { get; set; }

    public GetAllProductByNameQuery(string name)
    {
        Name = name;
    }
}

public class GetAllProductByNameQueryHandler(IProductRepository repository, IMapper mapper)
    : IRequestHandler<GetAllProductByNameQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductByNameQuery request,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetProductsByName(request.Name);
        return mapper.Map<IEnumerable<ProductResponse>>(result);
    }
}