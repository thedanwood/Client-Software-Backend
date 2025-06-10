using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Requests;
using HaulageSystem.Domain.Enums;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;

namespace HaulageSystem.Application.Domain.Services;

//TODO refactor into reusable service
public class PdfService: IPdfService
{
    public MemoryStream GenerateQuotePdf(GenerateQuotePdfRequest request)
    {
        using (var ms = new MemoryStream())
        {
            Document doc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();
            
            var lightBlueColor = WebColors.GetRGBColor("#e3eeff");
            string url = "https://roryjholbrook.co.uk/images/rjh-logo.jpg";
            Image img = Image.GetInstance(url);
            img.ScalePercent(50);
            img.SetAbsolutePosition(doc.Left, doc.Top);
            doc.Add(img);
            
            var spacer = new Paragraph("")
            {
                SpacingBefore = 10f,
                SpacingAfter = 10f,
            };
            doc.Add(spacer);
            
            PdfPTable infoTable = new PdfPTable(1)
            {
                //1: center, 1:left, 2:right,
                HorizontalAlignment = 2,
                WidthPercentage = 40,
                DefaultCell = { MinimumHeight = 22f, HorizontalAlignment = 2, Border = Rectangle.NO_BORDER }
            };
            infoTable.AddCell("Created by " + request.StaffFullName);
            infoTable.AddCell("01953 718306");
            infoTable.AddCell(request.StaffEmailAddress);
            infoTable.AddCell("www.roryjholbrook.co.uk");
            infoTable.AddCell("Roudham Rd, East Harling, Attleborough, Norwich NR16 2QN");

            PdfPTable headerTable = new PdfPTable(2)
            {
                //1: center, 1:left, 2:right,
                HorizontalAlignment = 0,
                WidthPercentage = 55,
                DefaultCell = { MinimumHeight = 22f, Border = Rectangle.NO_BORDER }
            };
            //column widths
            float[] widths = new float[] { 2f, 3f };
            headerTable.SetWidths(widths);
            
            headerTable.AddCell("Date:");
            headerTable.AddCell(request.CreatedDateTime.ToString("dd/MM/yyyy"));
            headerTable.AddCell("Name:");
            headerTable.AddCell(request.CompanyName);
            headerTable.AddCell("Delivery Address:");
            headerTable.AddCell(request.DeliveryLocationFullAddress);
            headerTable.AddCell("Quote Number:");
            headerTable.AddCell("Q" + request.QuoteId.ToString());
            headerTable.AddCell("Valid Until:");
            headerTable.AddCell(request.CreatedDateTime.AddDays(30).ToString("dd/MM/yyyy"));
            
            //add the 2 tables next to eachother
            PdfPTable doubleTable = new PdfPTable(2)
            {
                DefaultCell = { Border = Rectangle.NO_BORDER },
                WidthPercentage = 100,
            };
            doubleTable.AddCell(headerTable);
            doubleTable.AddCell(infoTable);
            doc.Add(doubleTable);
            doc.Add(spacer);
            
            var columnCount = 2;
            var columnWidths = new[] { 1f, 1f};
            PdfPTable quoteSummaryTable = new PdfPTable(columnCount)
            {
                //1: center, 1:left, 2:right,
                HorizontalAlignment = 1,
                WidthPercentage = 100,
                DefaultCell = { MinimumHeight = 22f }
            };
            quoteSummaryTable.SetWidths(columnWidths);
            
            var titleCell = new PdfPCell(new Phrase("Quote Summary"))
            {
                Colspan = columnCount,
                HorizontalAlignment = 1,
                MinimumHeight = 30,
            };
            quoteSummaryTable.AddCell(titleCell);
            
            string itemHeader = "";
            if (request.RecordType == RecordTypes.SupplyAndDelivery.ToInt())
            {
                itemHeader = "Material";
            }
            else if (request.RecordType == RecordTypes.DeliveryOnly.ToInt())
            {
                itemHeader = "Haulage Movement";
            }
            
            var materialCell = new PdfPCell(new Phrase(itemHeader));
            materialCell.BackgroundColor=lightBlueColor;
            
            string priceHeader = "";
            if(request.RecordType == RecordTypes.SupplyAndDelivery.ToInt())
            {
                priceHeader = "Material Price Including Delivery";
            }
            else if (request.RecordType == RecordTypes.DeliveryOnly.ToInt())
            {
                priceHeader = "Haulage Price";
            }
            var priceCell = new PdfPCell(new Phrase(priceHeader));
            priceCell.BackgroundColor = lightBlueColor;
            
            quoteSummaryTable.AddCell(materialCell);
            quoteSummaryTable.AddCell(priceCell);
            
            foreach (var deliveryMovement in request.Movements)
            {
                if (request.RecordType == RecordTypes.SupplyAndDelivery.ToInt())
                {
                    quoteSummaryTable.AddCell(deliveryMovement.VehicleTypeName +" - "+deliveryMovement.SupplyDeliveryInfo.MaterialName);

                    quoteSummaryTable.AddCell(deliveryMovement.SupplyDeliveryInfo.TotalMaterialAndDeliveryPricePerQuantityUnit.ToString());
                }
                else if (request.RecordType == RecordTypes.DeliveryOnly.ToInt())
                {
                    string haulageMovementInfoString = "Delivery from " + deliveryMovement.DeliveryInfo.StartLocationFullAddress+" to " + deliveryMovement.DeliveryInfo.DeliveryLocationFullAddress;
                    quoteSummaryTable.AddCell(deliveryMovement.VehicleTypeName+" - "+ haulageMovementInfoString);
                    quoteSummaryTable.AddCell(deliveryMovement.DeliveryInfo.DeliveryPricePerQuantityUnit.ToString());
                }
            }
            
            doc.Add(spacer);
            doc.Add(quoteSummaryTable);

            if (request.Comments != null)
            {                        
                doc.Add(spacer);

                doc.Add(new Paragraph("Comment(s): " + request.Comments));
            }
            doc.Add(spacer);

            doc.Add(new Paragraph("Subject to a fuel escalator"));
            doc.Add(spacer);
            doc.Add(new Paragraph("For and on behalf of Rory J Holbrook Limited"));
            doc.Add(new Paragraph("Rory J Holbrook Limited"));
            doc.Add(new Paragraph("07795 285483"));
            doc.Add(new Paragraph("Registered in England"));
            doc.Add(new Paragraph("Co. Reg. No. 04494452"));

            writer.CloseStream = false;
            doc.Close();
            ms.Position = 0;
            var bytes = ms.ToArray();
            return new MemoryStream(bytes);
        }
        
    }
}