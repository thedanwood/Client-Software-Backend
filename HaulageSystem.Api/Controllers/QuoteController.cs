using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Queries.Quotes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class QuoteController : BaseApiController
{
    private readonly IMediator _mediator;
    public QuoteController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("")]
    public async Task<ActionResult<List<GetQuoteDto>>> GetSavedQuotes()
    {
        return Ok(await _mediator.Send(new GetSavedQuotesQuery()));
    }

    [HttpGet("delivery-only/initial-data")]
    public async Task<ActionResult<QuoteDeliveryOnlyInitialDataDto>> GetDeliveryOnlyQuoteInitialData()
    {
        return Ok(await _mediator.Send(new GetDeliveryOnlyQuoteInitialDataQuery()));
    }

    [ProducesResponseType(typeof(QuoteSupplyDeliveryInitialDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("supply-delivery/initial-data")]
    public async Task<ActionResult<QuoteSupplyDeliveryInitialDataDto>> GetSupplyDeliveryQuoteInitialData()
    {
        return Ok(await _mediator.Send(new GetSupplyDeliveryQuoteInitialDataQuery()));
    }
    
    [HttpGet("delivery-only/initial-data/{id}")]
    public async Task<ActionResult<UpdateQuoteDeliveryOnlyInitialDataDto>> GetDeliveryOnlyUpdateQuoteInitialData([FromRoute]int id)
    {
        return Ok(await _mediator.Send(new GetDeliveryOnlyUpdateQuoteInitialDataQuery(id)));
    }
    [HttpGet("supply-delivery/initial-data/{id}")]
        public async Task<ActionResult<UpdateQuoteSupplyDeliveryInitialDataDto>> GetSupplyDeliveryUpdateQuoteInitialData([FromRoute]int id)
        {
            return Ok(await _mediator.Send(new GetSupplyDeliveryUpdateQuoteInitialDataQuery(id)));
        }
    
    [HttpPut("delivery-only/{quoteId:int}")]
    public async Task<ActionResult> UpdateDeliveryOnlyQuote([FromRoute]int quoteId,
        [FromBody] UpdateDeliveryOnlyQuoteCommand command)
    {
        command.QuoteId = quoteId;
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("delivery-only/pricing/{quoteId:int}")]
    public async Task<ActionResult> UpdateDeliveryOnlyQuotePricing([FromRoute]int quoteId,
        [FromBody] UpdateDeliveryOnlyQuotePricingCommand command)
    {
        command.QuoteId = quoteId;
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("supply-delivery/{quoteId:int}")]
    public async Task<ActionResult> UpdateSupplyDeliveryQuote([FromRoute] int quoteId,
        [FromBody] UpdateSupplyDeliveryQuoteCommand command)
    {
        command.QuoteId = quoteId;
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("supply-delivery/pricing/{quoteId:int}")]
    public async Task<ActionResult> UpdateSupplyDeliveryQuotePricing([FromRoute]int quoteId,
        [FromBody] UpdateSupplyDeliveryQuotePricingCommand command)
    {
        command.QuoteId = quoteId;
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPost("delivery-only/pricing")]
    public async Task<ActionResult<AdjustDeliveryQuotePricingDto>> GetNewDeliveryOnlyMovementPricing([FromBody] GetNewDeliveryOnlyMovementPricingQuery query)
    {
        var x = Ok(await _mediator.Send(query));
        return x;
    }
    
    [HttpPost("delivery-only/pricing/{quoteId:int}")]
    public async Task<ActionResult<List<DeliveryOnlyMovementPricingDto>>> GetSavedDeliveryOnlyMovementPricings([FromRoute]int quoteId)
    {
        var query = new GetSavedDeliveryOnlyMovementPricingsQuery();
        query.QuoteId = quoteId;
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost("supply-delivery/pricing")]
    public async Task<ActionResult<SupplyDeliveryMovementPricingDto>> GetNewSupplyDeliveryMovementPricing([FromBody] GetNewSupplyDeliveryMovementPricingQuery query)
    {
        var x = Ok(await _mediator.Send(query));
        return x;
    }
    
    [HttpPost("supply-delivery/pricing/{quoteId:int}")]
    public async Task<ActionResult<List<SupplyDeliveryMovementPricingDto>>> GetSavedSupplyDeliveryMovementPricings([FromRoute]int quoteId)
    {
        var query = new GetSavedSupplyDeliveryMovementPricingsQuery();
        query.QuoteId = quoteId;
        var x = Ok(await _mediator.Send(query));
        return x;
    }

    [HttpPost("delivery-only")]
    public async Task<ActionResult<int>> CreateDeliveryOnlyQuote([FromBody] CreateDeliveryOnlyQuoteCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("supply-delivery")]
    public async Task<ActionResult<int>> CreateSupplyDeliveryQuote([FromBody] CreateSupplyDeliveryQuoteCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [ProducesResponseType(typeof(List<MaterialMovementForDisplayDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("supply-delivery/movement/initial-data")]
    public async Task<ActionResult<List<MaterialMovementForDisplayDto>>> GetAddedMaterialMovementInitialData([FromQuery]GetMaterialMovementInitialDataQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
    [ProducesResponseType(typeof(DeliveryOnlyMovementForDisplayDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("delivery-only/movement/initial-data")]
    public async Task<ActionResult<DeliveryOnlyMovementForDisplayDto>> GetAddedDeliveryOnlyMovementInitialData([FromQuery]GetDeliveryOnlyMovementInitialDataQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
    
    [HttpPost("download/{quoteId:int}")]
    public async Task<ActionResult> DownloadQuote([FromRoute] int quoteId)
    {
        var query = new DownloadQuoteQuery();
        query.QuoteId = quoteId;
        var result = await _mediator.Send(query);
        return File(result.Bytes, "application/pdf");
    }
    
    [HttpPost("email/{quoteId:int}")]
    public async Task<ActionResult> EmailQuote([FromRoute] int quoteId, [FromBody]EmailQuoteCommand command)
    {
        command.QuoteId = quoteId;
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuote([FromRoute] int id)
    {
        var query = new DeleteQuoteCommand() { QuoteId = id};
        await _mediator.Send(query);
        return Ok();
    }
}