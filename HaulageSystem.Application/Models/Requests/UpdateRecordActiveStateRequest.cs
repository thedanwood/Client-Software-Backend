using HaulageSystem.Application.Domain.Entities.Database;

namespace HaulageSystem.Application.Models.Requests;

public class UpdateRecordActiveStateRequest
{
    public int QuoteId { get; set; }
    public int ActiveState { get; set; }
}