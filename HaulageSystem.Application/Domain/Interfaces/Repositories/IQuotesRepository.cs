using HaulageSystem.Application.Domain.Entities.Quotes;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Application.Dtos.Quotes;
using OneOf;
using OneOf.Types;
using HaulageSystem.Domain.Enums;

namespace HaulageSystem.Application.Domain.Interfaces.Repositories;

public interface IQuotesRepository
{
    Task<List<GetQuoteResponse>> GetTestQuotes();

    Task<int> CreateRecordAsync(CreateRecordRequest request);
    Task<int> CreateDeliveryMovement(CreateDeliveryMovementRequest request);
    Task DeleteDeliveryMovement(int deliveryMovementId);
    Task DeleteMaterialMovements(List<int> MaterialMovementIds);
    Task DeleteMaterialMovement(int MaterialMovementId);
    Task<int> CreateMaterialMovement(CreateMaterialMovementRequest request);
    Task UpdateQuote(UpdateRecordRequest request);
    Task UpdateDeliveryMovement(UpdateDeliveryMovementRequest request);
    Task UpdateMaterialMovement(UpdateMaterialMovementRequest request);
    Task UpdateQuoteActiveState(UpdateRecordActiveStateRequest activeStateRequest);
    Task<List<GetQuoteResponse>> GetQuotes(int fromDaysAgo);
    Task<GetQuoteResponse> GetQuote(int quoteId);
    Task<List<GetDeliveryMovementResponse>> GetDeliveryMovementsAsync(int quoteId);
    Task<GetDeliveryMovementResponse> GetDeliveryMovementAsync(int deliveryMovementId);
    Task<List<GetDeliveryMovementResponse>> GetDeliveryMovementsAsync(List<int> quoteIds);
    Task<GetMaterialMovementResponse> GetMaterialMovementAsync(int deliveryMovementId);
    Task<List<GetMaterialMovementResponse>> GetMaterialMovementsAsync(List<int> deliveryMovementIds);
    Task<List<GetMaterialMovementResponse>> GetMaterialMovementsAsync(int deliveryMovementId);

    Task SetActiveState(int quoteId, int activeState);
}