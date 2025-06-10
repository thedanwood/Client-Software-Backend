using System.Globalization;
using HaulageSystem.Application.Models.Depots;
using HaulageSystem.Domain.Enums;
using Serilog;

namespace HaulageSystem.Application.Exceptions;

public class MailSendException: Exception
{
    public MailSendException(string message): base(message)
    {
    }
}