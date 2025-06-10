using System.Data;
using System.Xml;
using HaulageSystem.Application.Domain.Dtos.Companies;
using HaulageSystem.Application.Domain.Dtos.Materials;
using HaulageSystem.Application.Domain.Entities.Companies;
using HaulageSystem.Application.Domain.Entities.Database;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Extensions;
using HaulageSystem.Domain.Interfaces;
using HaulageSystem.Application.Dtos.Quotes;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Quotes;
using HaulageSystem.Application.Models.Routing;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Queries.Quotes;

public class GetSavedQuotesQueryHandler : IRequestHandler<GetSavedQuotesQuery, List<GetQuoteDto>>
{
    private readonly IQuotesRepository _quoteRepository;
    private readonly IUserService _userService;
    private readonly IMaterialsRepository _materialsRepository;
    private readonly IMaterialsService _materialsService;
    private readonly IDepotsRepository _depotsRepository;
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IMaterialPricingRepository _materialPricingRepository;
    private readonly IProfileService _profileService;
    private readonly IQuotesService _quotesService;

    public GetSavedQuotesQueryHandler(IQuotesRepository quotesRepository, IUserService userService,IMaterialsRepository materialsRepository, IQuotesService quotesService, IMaterialPricingRepository materialPricingRepository, IMaterialsService materialsService, IDepotsRepository depotsRepository, IVehiclesRepository vehiclesRepository, ICompaniesRepository companyInfo, IProfileService profileService)
    {
        _quoteRepository = quotesRepository;
        _userService = userService;
        _quotesService = quotesService;
        _materialPricingRepository = materialPricingRepository;
        _materialsService = materialsService;
        _depotsRepository = depotsRepository;
        _vehiclesRepository = vehiclesRepository;
        _companiesRepository = companyInfo;
        _profileService = profileService;
        _materialsRepository = materialsRepository;
    }

    public async Task<List<GetQuoteDto>> Handle(GetSavedQuotesQuery query, CancellationToken cancellationToken)
    {
        var daysAgo = _profileService.GetQuotesFromDaysAgo();
        var quotes = await _quoteRepository.GetQuotes(daysAgo);
        var quoteIds = quotes.Select(x => x.QuoteId).ToList();
        var customerIds = quotes.Select(x => x.CompanyID).Distinct().ToList();

        var deliveryMovements = await _quoteRepository.GetDeliveryMovementsAsync(quoteIds);
        var deliveryMovementIds = deliveryMovements.Select(x => x.DeliveryMovementId).ToList();
        var companies = await _companiesRepository.GetCompaniesAsync(customerIds);

        var materialMovements = await _quoteRepository.GetMaterialMovementsAsync(deliveryMovementIds);
        var DepotMaterialPriceIds = materialMovements.Select(x => x.DepotMaterialPriceId).ToList();
        var vehicleTypes = await _vehiclesRepository.GetAllVehicleTypes();

        var pricings =
            await _materialPricingRepository.GetMaterialPricingsByDepotMaterialPriceIdsAsync(DepotMaterialPriceIds);
        var materialIds = pricings.Select(x => x.MaterialId).ToList();
        var depotIds = pricings.Select(x => x.DepotId).ToList();
        
        var materialUnits = await _materialsService.GetMaterialUnits();
        var materials = await _materialsRepository.GetMaterials(materialIds);
        var depots = await _depotsRepository.GetDepots(depotIds, true);
        
        var dtos = new List<GetQuoteDto>();
        foreach (var quote in quotes.ToList())
        {
            var userFullName = await _userService.GetFullName(quote.CreatedByUsername);
            var quoteDeliveryMovements = deliveryMovements.Where(x => x.QuoteId == quote.QuoteId).ToList();
            var quoteMaterialMovements = materialMovements.Where(x => quoteDeliveryMovements.Select(x=>x.DeliveryMovementId).ToList().Contains(x.DeliveryMovementId)).ToList();
            var firstQuoteDeliveryMovement = quoteDeliveryMovements[0];
            var company = companies.FirstOrDefault(x => x.CompanyId == quote.CompanyID);
            var totalPrice = quoteDeliveryMovements.Sum(x => x.TotalDeliveryPrice) + quoteMaterialMovements.Sum(x=>x.TotalMaterialPrice);
            totalPrice = totalPrice.ToPrice();
            
            var dto = new GetQuoteDto()
            {
                QuoteId = quote.QuoteId,
                QuoteNumber = quote.QuoteId,
                QuoteType = (RecordTypes)quote.RecordType,
                CreationInfo = new()
                {
                    CustomerName = quote.CustomerName,
                    Company = new CompanyDto()
                    {
                        Id = quote.CompanyID,
                        Name = company.CompanyName,
                        Email = company.Email,
                        Phone = company.PhoneNumber
                    },
                    CreatedDateTime = quote.CreatedDateTime,
                    CreatedByName = userFullName, 
                },
                DeliveryInfo = new()
                {
                    DeliveryDate = FormatDeliveryDate(quote.DeliveryDate),
                    DeliveryLocation = quote.DeliveryLocationFullAddress,
                },
                Comments = quote?.Comments,
                ActiveState = EnumHelpers.GetActiveStateDisplayableText(quote.ActiveStateEnumValue),
                TotalQuotePrice = totalPrice,
            };

            var movements = new List<GetQuoteMovementDto>();
            foreach (var deliveryMovement in quoteDeliveryMovements)
            {
                var vehicle = vehicleTypes.FirstOrDefault(x => x.Id == deliveryMovement.VehicleTypeId);
                var movement = new GetQuoteMovementDto()
                {
                    DeliveryMovement = new()
                    {
                        DeliveryMovementId = deliveryMovement.DeliveryMovementId,
                        VehicleType = new (vehicle.Id, vehicle.Name, vehicle.Capacity),
                        NumberOfLoads = deliveryMovement.NumberOfLoads,
                        OnewayJourneyTime = deliveryMovement.OnewayJourneyTime
                    },
                };
                
                if (quote.RecordType == RecordTypes.DeliveryOnly.ToInt())
                {
                    var startLocation = new AddressDto(
                        new RoutePoint(deliveryMovement.StartLocationLatitude.Value,
                            deliveryMovement.StartLocationLongitude.Value),
                        deliveryMovement.StartLocationFullAddress
                    );
                    movement.DeliveryMovement.StartLocation = startLocation;
                }
                if (quote.RecordType == RecordTypes.SupplyAndDelivery.ToInt())
                {
                    var materialMovement = quoteMaterialMovements.FirstOrDefault(x =>
                        x.DeliveryMovementId == deliveryMovement.DeliveryMovementId);
                    var materialPricing =
                        pricings.FirstOrDefault(x => x.DepotMaterialPriceId == materialMovement.DepotMaterialPriceId);
                    var material = materials.FirstOrDefault(x => x.MaterialId == materialPricing.MaterialId);
                    var materialUnit = materialUnits.FirstOrDefault(x => x.UnitId == materialMovement.MaterialUnitId);
                    var depot = depots.FirstOrDefault(x => x.DepotId == materialPricing.DepotId);

                    movement.MaterialMovement = new()
                    {
                        Material = new(material.MaterialId, material.MaterialName),
                        Quantity = materialMovement.Quantity,
                        MaterialUnit = new(materialUnit.UnitId, materialUnit.UnitName),
                        TotalPrice = materialMovement.TotalMaterialPrice,
                        PricePerQuantityUnit = materialMovement.MaterialPricePerQuantityUnit,
                        DepotName = depot.DepotName,
                    };
                }

                movements.Add(movement);
                dto.Movements = movements;
            }
            
            dtos.Add(dto);
        }

        return dtos;
    }

    private DateTime? FormatDeliveryDate(DateTime? deliveryDate)
    {
        if (deliveryDate.Equals(DateTime.MinValue))
        {
            return null;
        }
        else
        {
            return deliveryDate;
        }
    }
}