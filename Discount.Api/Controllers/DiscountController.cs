using Discount.Application.Protos.discount.proto;
using Discount.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers;

public class DiscountController(IMediator mediator) : ApiController
{
    [HttpGet("{productName}")]
    public async Task<ActionResult<CouponModel>> GetDiscountByUserName(string productName,CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetDiscountByProductNameQuery(productName),ct));
    }
    
    [HttpGet("{productId}")]
    public async Task<ActionResult<CouponModel>> GetDiscountById(string productId,CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetDiscountByProductIdQuery(productId),ct));
    }
}