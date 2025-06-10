using HaulageSystem.Application.Core.Commands.Companies;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Repositories;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using MediatR;

namespace HaulageSystem.Application.Commands.Quotes;

public class EmailQuoteCommandHandler : IRequestHandler<EmailQuoteCommand, bool>
{
    private readonly IUserService _userService;
    private readonly IMediator _mediator;
    private readonly IMailService _mailService;
    private readonly IQuotesRepository _quotesRepository;
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IProfileService _profileService;

    public EmailQuoteCommandHandler(IMediator mediator, IMailService mailService, IQuotesRepository quotesRepository,
        ICompaniesRepository companiesRepository, IUserService userService, IProfileService profileService)
    {
        _mediator = mediator;
        _mailService = mailService;
        _quotesRepository = quotesRepository;
        _companiesRepository = companiesRepository;
        _userService = userService;
        _profileService = profileService;
    }

    public async Task<bool> Handle(EmailQuoteCommand command, CancellationToken cancellationToken)
    {
        var currentUser = await _userService.GetUserInfo(await _userService.GetCurrentUsername());
        var quote = await _quotesRepository.GetQuote(command.QuoteId);
        var createdQuoteUser = await _userService.GetUserInfo(quote.CreatedByUsername);
        var company = await _companiesRepository.GetCompanyByIdAsync(quote.CompanyID);
        var fromEmail = _profileService.GetEmails()[EmailUseCase.SendQuote];
        var emailConfig = await _profileService.GetEmailQuoteSettings();
        
        if (company == null)
        {
            throw new CompanyNotFoundException(quote.CompanyID);
        }

        var emailBody = $"See the attached quote created by {createdQuoteUser.FullName} for ";
        if (quote.CustomerName is not null)
        {
            emailBody += $"{quote.CustomerName} at ";
        }

        emailBody += company.CompanyName;

        var file = await _mediator.Send(new DownloadQuoteQuery() { QuoteId = command.QuoteId });
        var request = new SendEmailRequest()
        {
            FromEmail = new(fromEmail, "Rory J Holbrook ltd"),
            ToEmail = new(command.CompanyEmail, company.CompanyName),
            ReplyToEmail = new(currentUser.Email, currentUser.FullName),
            CcEmails = emailConfig.CcEmails,
            Subject = $"Quote #{quote.QuoteId} from Rory J Holbrook ltd",
            Body = emailBody,
            Attachments = new List<SendEmailAttachmentRequest>()
            {
                new()
                {
                    Bytes = file.Bytes,
                    FileName = $"Quote#{quote.QuoteId}_RoryJHolbrook.pdf",
                    Type = "application/pdf"
                }
            },
        };
        return await _mailService.SendEmail(request);
    }
}