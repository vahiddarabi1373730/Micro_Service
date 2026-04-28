using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductTypeQuery:IRequest<IEnumerable<ProductTypeResponse>>
{
    
}
public class GetAllProductTypeQueryHandler(IProductTypeRepository repository,IMapper mapper):IRequestHandler<GetAllProductTypeQuery,IEnumerable<ProductTypeResponse>>
{
    public async Task<IEnumerable<ProductTypeResponse>> Handle(GetAllProductTypeQuery request, CancellationToken cancellationToken)
    {
        var result =await repository.GetProductTypes();
        return mapper.Map<IEnumerable<ProductTypeResponse>>(result);
    }
}