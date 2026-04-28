using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Pagination;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductQuery :CatalogSpecParams ,IRequest<Pagination<ProductResponse>>
{
}

public class GetAllProductQueryHandler(IProductRepository repository, IMapper mapper)
    : IRequestHandler<GetAllProductQuery, Pagination<ProductResponse>>
{
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductQuery request,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetProducts(request);
        return mapper.Map<Pagination<ProductResponse>>(result);
    }
}