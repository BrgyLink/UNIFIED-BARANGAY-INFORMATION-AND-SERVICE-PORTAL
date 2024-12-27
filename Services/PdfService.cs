using DinkToPdf;
using DinkToPdf.Contracts;
using System.Text;

namespace BrgyLink.Services
{
    public class PdfService
    {
        private readonly IConverter _converter;

        public PdfService()
        {
            _converter = new BasicConverter(new PdfTools());
        }

        public byte[] GenerateBlotterPdf(string incident, string complainant, string respondent, DateTime dateReported, string actionTaken, string status)
        {
            var htmlContent = new StringBuilder();
            htmlContent.Append("<html><body>");
            htmlContent.Append("<h1>Barangay Blotter Report</h1>");
            htmlContent.Append($"<p><strong>Incident:</strong> {incident}</p>");
            htmlContent.Append($"<p><strong>Complainant:</strong> {complainant}</p>");
            htmlContent.Append($"<p><strong>Respondent:</strong> {respondent}</p>");
            htmlContent.Append($"<p><strong>Date Reported:</strong> {dateReported.ToString("MMMM dd, yyyy")}</p>");
            htmlContent.Append($"<p><strong>Action Taken:</strong> {actionTaken}</p>");
            htmlContent.Append($"<p><strong>Status:</strong> {status}</p>");
            htmlContent.Append("</body></html>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = { ColorMode = ColorMode.Color, Orientation = Orientation.Portrait, PaperSize = PaperKind.A4 },
                Objects = { new ObjectSettings() { HtmlContent = htmlContent.ToString(), WebSettings = { DefaultEncoding = "utf-8" } } }
            };

            return _converter.Convert(doc);
        }

    }
}
