using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

public class BasketController(IMediator mediator) : ApiController
{
    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName,CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetBasketQuery(userName),ct));
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody]  CreateBasketCommand command,CancellationToken ct)
    {
        var commandDebug = command;
        var result = await mediator.Send(commandDebug, ct);
        return Ok(result);
    }
    
    [HttpPut("{userName}")]
    public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody]  UpdateBasketCommand command,CancellationToken ct)
    {
        var commandDebug = command;
        var result = await mediator.Send(commandDebug, ct);
        return Ok(result);
    }

    [HttpDelete("{userName}")]
    public async Task<ActionResult<bool>> DeleteBasket(string userName, CancellationToken ct)
    {
        return Ok(await mediator.Send(new DeleteBasketCommand(userName),ct));
    }
}