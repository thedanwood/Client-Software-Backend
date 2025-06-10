namespace HaulageSystem.Application.Models.Requests;

public class SendEmailRequest
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public SendEmailAddressRequest FromEmail { get; set; }
    public SendEmailAddressRequest ToEmail { get; set; }
    public List<SendEmailAddressRequest> CcEmails { get; set; }
    public SendEmailAddressRequest? ReplyToEmail { get; set; }
    public List<SendEmailAttachmentRequest> Attachments { get; set; }
}

public class SendEmailAddressRequest
{
    public SendEmailAddressRequest(string email, string name)
    {
        Email = email;
        Name = name;
    }
    public string Email { get; set; }
    public string Name { get; set; }
}
public class SendEmailAttachmentRequest
{
    public byte[] Bytes { get; set; }
    public string FileName { get; set; }
    public string Type { get; set; }
}