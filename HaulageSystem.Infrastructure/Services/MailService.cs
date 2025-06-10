using HaulageSystem.Application.Configuration.ApiOptions;
using HaulageSystem.Application.Core.Commands.Quotes;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Exceptions;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Infrastructure.DependencyInjection.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HaulageSystem.Application.ApiClients;

public class MailService: IMailService
{
    private readonly SendGridOptions _apiOptions;
    public MailService(IOptions<SendGridOptions> apiOptions)
    {
        _apiOptions = apiOptions?.Value ?? throw new ArgumentNullException(nameof(apiOptions));
    }
    
    public async Task<bool> SendEmail(SendEmailRequest request)
    {
        var client = new SendGridClient(_apiOptions.ApiKey);
        var from = new EmailAddress(request.FromEmail.Email, request.FromEmail.Name);
        var to = new EmailAddress(request.ToEmail.Email, request.ToEmail.Name);

        var message = MailHelper.CreateSingleEmail(from, to, request.Subject, request.Body, "");

        if (request.ReplyToEmail is not null)
        {
            var replyTo = new EmailAddress(request.ReplyToEmail.Email, request.ReplyToEmail.Name);
            message.SetReplyTo(replyTo);
        }

        if (request.CcEmails.Any())
        {
            var ccs = request.CcEmails.Select(x => new EmailAddress(x.Email, x.Name)).ToList();
            message.AddCcs(ccs);
        }

        if (request.Attachments.Any())
        {
            message.Attachments = request.Attachments.Select(x => new Attachment()
            {
                Content = Convert.ToBase64String(x.Bytes),
                Filename = x.FileName,
                Type = x.Type,
                Disposition = "attachment"
            }).ToList();
        }

        var response = await client.SendEmailAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            throw new MailSendException($"Error sending email. Status code: {response.StatusCode}");
        }
        
        return response.IsSuccessStatusCode;
    }
}