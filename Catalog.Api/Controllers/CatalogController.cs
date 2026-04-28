using Catalog.Application.Commands.Products;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

public class CatalogController(IMediator mediator):ApiController
{
    [HttpGet("${id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetProductByIdQuery(id), cancellationToken));
    }
    
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProduct([FromQuery]GetAllProductQuery request,CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken)); 
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<ProductResponse>> GetAllProductByName(string name,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllProductByNameQuery(name),cancellationToken));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductBrandResponse>>> GetAllBrand(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllProductBrandQuery(),cancellationToken));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductTypeResponse>>> GetAllType(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllProductTypeQuery(),cancellationToken));
    }
    
    [HttpGet("{brandName}")]
    public async Task<ActionResult<IEnumerable<ProductTypeResponse>>> GetAllProductByBrandName(string brandName,CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllProductByBrandNameQuery(brandName),cancellationToken));
    }
    
    [HttpGet("{typeName}")]
    public async Task<ActionResult<IEnumerable<ProductTypeResponse>>> GetAllProductByTypeName(string typeName,CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllProductByTypeNameQuery(typeName),cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] AddProductCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProduct(string id,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new DeleteProductCommand(id), cancellationToken));
    }
}