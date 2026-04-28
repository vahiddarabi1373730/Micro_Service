using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;
public class GetAllProductByBrandNameQuery:IRequest<IEnumerable<ProductResponse>>
{
    public string Name { get; set; }

    public GetAllProductByBrandNameQuery(string name)
    {
        Name = name;
    }
}
public class GetAllProductByBrandNameQueryHandler(IProductRepository repository,IMapper mapper):IRequestHandler<GetAllProductByBrandNameQuery,IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductByBrandNameQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetProductsByBrandName(request.Name);
        return mapper.Map<IEnumerable<ProductResponse>>(result);
    }
}