using System.Globalization;
using HaulageSystem.Application.Core.Commands.Materials;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Core.Domain.Calculator;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Dtos.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Dtos.Materials;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

namespace HaulageSystem.Application.Commands.Quotes;

public class DownloadQuoteQueryHandler : IRequestHandler<DownloadQuoteQuery, DownloadQuotePdfDto>
{
    private readonly IPdfService _pdfService;
    private readonly IQuotesRepository _quotesRepository;
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IUserService _userService;
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IMaterialsRepository _materialsRepository;

    public DownloadQuoteQueryHandler(IPdfService pdfService, IQuotesRepository quotesRepository, ICompaniesRepository companiesRepository, IUserService userService, IVehiclesRepository vehiclesRepository, IMaterialPricingRepository materialPricingRepository, IMaterialsRepository materialsRepository)
    {
        _pdfService = pdfService;
        _quotesRepository = quotesRepository;
        _companiesRepository = companiesRepository;
        _userService = userService;
        _vehiclesRepository = vehiclesRepository;
        _materialPricingRepository = materialPricingRepository;
        _materialsRepository = materialsRepository;
    }

    public async Task<DownloadQuotePdfDto> Handle(DownloadQuoteQuery query,
        CancellationToken cancellationToken)
    {
        var quote = await _quotesRepository.GetQuote(query.QuoteId);
        var company = await _companiesRepository.GetCompanyByIdAsync(quote.CompanyID);
        var staffFullName = await _userService.GetFullName(quote.CreatedByUsername);
        var staffEmail = await _userService.GetEmailAddress(quote.CreatedByUsername);

        var deliveryMovements = await _quotesRepository.GetDeliveryMovementsAsync(quote.QuoteId);
        var pdfRequestMovements = new List<MovementQuotePdfRequest>();
        foreach (var deliveryMovement in deliveryMovements)
        {
            var vehicle = await _vehiclesRepository.GetVehicleType(deliveryMovement.VehicleTypeId);
            var request = new MovementQuotePdfRequest()
            {
                VehicleTypeName = vehicle.Name,
                DeliveryInfo = new()
                {
                    DeliveryLocationFullAddress = quote.DeliveryLocationFullAddress,
                }
            };
            
            if (quote.RecordType == RecordTypes.DeliveryOnly.ToInt())
            {
                var vehicleTypeQuantity = await _vehiclesRepository.GetVehicleCapacity(deliveryMovement.VehicleTypeId);
                request.DeliveryInfo.StartLocationFullAddress = deliveryMovement.StartLocationFullAddress;
                request.DeliveryInfo.DeliveryPricePerQuantityUnit =
                    QuoteCalculator.CalculateDeliveryPricePerQuantityUnit(deliveryMovement.TotalDeliveryPrice,
                        vehicleTypeQuantity);
            }

            if (quote.RecordType == RecordTypes.SupplyAndDelivery.ToInt())
            {
                var materialMovement = await  _quotesRepository.GetMaterialMovementAsync(deliveryMovement.DeliveryMovementId);
                var materialPricing =
                    await _materialPricingRepository.GetMaterialPricingByDepotMaterialPriceId(materialMovement
                        .DepotMaterialPriceId);
                var material = await _materialsRepository.GetMaterial(materialPricing.MaterialId);

                var totalMaterialAndDeliveryPrice =
                    QuoteCalculator.CalculateTotalMaterialAndDeliveryPrice(materialMovement.TotalMaterialPrice,
                        deliveryMovement.TotalDeliveryPrice);
                var materialAndDeliveryPricePerQuantityUnit =
                    QuoteCalculator.CalculateMaterialAndDeliveryPricePerQuantityUnit(totalMaterialAndDeliveryPrice,
                        (materialMovement.Quantity * deliveryMovement.NumberOfLoads));

                request.SupplyDeliveryInfo = new()
                {
                    MaterialName = material.MaterialName,
                    TotalMaterialAndDeliveryPricePerQuantityUnit = materialAndDeliveryPricePerQuantityUnit
                };
            }

            pdfRequestMovements.Add(request);
        }

        var quotePdfRequest = new GenerateQuotePdfRequest()
        {
            CreatedDateTime = quote.CreatedDateTime,
            CompanyName = company.CompanyName,
            DeliveryLocationFullAddress = quote.DeliveryLocationFullAddress,
            QuoteId = quote.QuoteId,
            RecordType = quote.RecordType,
            Comments = quote.Comments,
            StaffEmailAddress = staffEmail,
            StaffFullName = staffFullName,
            Movements = pdfRequestMovements,
        };
        
        var memoryStream = _pdfService.GenerateQuotePdf(quotePdfRequest);
        
        return new DownloadQuotePdfDto()
        {
            Bytes = memoryStream.ToArray(),
        };
    }
}