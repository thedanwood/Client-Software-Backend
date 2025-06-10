using Microsoft.AspNetCore.Mvc;

namespace HaulageSystem.Application.Dtos.Quotes;

public class DownloadQuotePdfDto
{
    public byte[] Bytes { get; set; }
}