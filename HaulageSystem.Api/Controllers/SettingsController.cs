using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Core.Commands.Settings;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Dtos.Settings;
using HaulageSystem.Application.Queries.Quotes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class SettingsController : BaseApiController
{
    private readonly IMediator _mediator;
    public SettingsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    
    [HttpGet("")]
    public async Task<ActionResult<SettingsInitialDataDto>> GetInitialData()
    {
        return Ok(await _mediator.Send(new GetSettingsInitialDataQuery()));
    }
}