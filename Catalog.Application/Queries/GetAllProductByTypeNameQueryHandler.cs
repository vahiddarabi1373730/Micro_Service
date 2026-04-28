using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;
public class GetAllProductByTypeNameQuery:IRequest<IEnumerable<ProductResponse>>
{
    public string Name { get; set; }

    public GetAllProductByTypeNameQuery(string name)
    {
        Name = name;
    }
}
public class GetAllProductByTypeNameQueryHandler(IProductRepository repository,IMapper mapper):IRequestHandler<GetAllProductByTypeNameQuery,IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductByTypeNameQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetProductsByTypeName(request.Name);
        return mapper.Map<IEnumerable<ProductResponse>>(result);
    }
}