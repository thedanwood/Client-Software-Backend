using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : Controller
{
}