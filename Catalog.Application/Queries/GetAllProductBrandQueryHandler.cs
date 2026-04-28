using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;
public class GetAllProductBrandQuery:IRequest<IEnumerable<ProductBrandResponse>>
{
    
}

public class GetAllProductBrandQueryHandler(IProductBrandRepository repository,IMapper mapper):IRequestHandler<GetAllProductBrandQuery,IEnumerable<ProductBrandResponse>>
{
    public async Task<IEnumerable<ProductBrandResponse>> Handle(GetAllProductBrandQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetProductBrands();
        return mapper.Map<IEnumerable<ProductBrandResponse>>(result);
    }
}   