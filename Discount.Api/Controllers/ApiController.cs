using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class ApiController:ControllerBase {}