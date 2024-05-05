using AutoMapper;
using DinkToPdf;
using LinkQuality.Data;
using System.Text;
using Microsoft.AspNetCore.Http;

using static System.Runtime.InteropServices.JavaScript.JSType;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Threading;
using System.Data;
using ClosedXML.Excel;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using Data;
using PuntoDeVentaAPI.Services;

namespace LinkQuality.Api.Services
{
    public class ReportsService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        static readonly SynchronizedConverter _converter = new(new PdfTools());
        public readonly string _usuario;
        public readonly string _ip;
        //private static string path = HostingEnvironment.MapPath("~/Images/");

        public ReportsService(IConfiguration configuration, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, ApplicationUserManager userManager, string usuario, string ip)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _ip = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _usuario = Task.Run(async () =>
            (await userManager.FindByNameAsync(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
            c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;

        }

      
        //public MemoryStream PdfReporteInspecciones(ResponseInspectionFormDTO infoInspection, List<string> UrlsPhotosInspection)
        //{
        //    try
        //    {
        //        //string imagePath = Path.Combine("IMG", "logo-reportes-color-sinfondo-01.png");
        //        string sharepointImageUrl = "https://i.ibb.co/xYgG64t/logo-reportes-color-sinfondo-01-1.png";

        //        var htmlContent = "<!DOCTYPE html>" +
        //            "<html lang='es'>" +
        //            "<head>" +
        //            "<meta charset='UTF-8'>" +
        //            "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
        //            "<title>Reporte PDF</title>" +
        //            "<style>" +
        //            "body {" +
        //            "   font-family: Arial, sans-serif;" +
        //            "   margin: 20px;" +
        //            "}" +
        //            "table {" +
        //            "   width: 100%;" +
        //            "   border-collapse: collapse;" +
        //            "}" +
        //            "th, td {" +
        //            "   padding: 8px;" +
        //            "   border: 1px solid #dddddd;" +
        //            "   text-align: left;" +
        //            "}" +
        //            "th {" +
        //            "   background-color: #f2f2f2;" +
        //            "}" +
        //            "img {" +
        //            "   max-width: 100%;" +
        //            "   height: auto;" +
        //            "}" +
        //            ".watermark {" +
        //            "   position: fixed;" +
        //            "   top: 50%;" +
        //            "   left: 50%;" +
        //            "   transform: translate(-50%, -50%);" +
        //            "   opacity: 0.5;" +
        //            "}" +
        //            "</style>" +
        //            "</head>" +
        //            "<body>";

        //        // Logo en la esquina superior derecha
        //        //htmlContent += "<img src='C:\\Users\\AppDev\\Documents\\repo\\LinkQualityApi\\LinkQuality.Data\\Img\\logo-reportes-color-sinfondo-01.png' style='position: absolute; top: 0px; right: 20px; width: 140px; altura: 140px'>";
        //        htmlContent += $"<img src='{sharepointImageUrl}' style='position: absolute; top: -40px; right: 20px; width: 200px; altura: 200px'>";

        //        var nameForm = infoInspection.NameInspectionForm;
        //        if (infoInspection.NamePresentationType != null && !infoInspection.NamePresentationType.Equals(""))
        //        {
        //            nameForm += $" - {infoInspection.NamePresentationType}";
        //        }

        //        // Título del informe
        //        htmlContent += "<div style='width:100%; text-align:center; margin-bottom: 20px;'>";
        //        htmlContent += $"<h2>Reporte Inspección</h2>";
        //        htmlContent += $"<h3>{nameForm}</h3>";
        //        htmlContent += "</div>";

        //        // Contenido de la tabla
        //        htmlContent += "<div style='width:100%;'>";
        //        htmlContent += "<table style='width:100%; border-collapse: separate; border-spacing: 0'>";
        //        //htmlContent += "<thead>";0
        //        //htmlContent += "<tr style='background-color:#808080; color:black;'>";
        //        //htmlContent += "<th colspan='2' style='text-align:center; padding:8px;'>Resultados</th>";
        //        //htmlContent += "</tr>";
        //        //htmlContent += "</thead>";
        //        htmlContent += "<tbody>";

        //        // Sección de preguntas y respuestas
        //        foreach (var group in infoInspection.groups)
        //        {
        //            int countRowQuestion = 0; // Inicializar countRowQuestion
        //            htmlContent += "<tr>";
        //            htmlContent += $"<td colspan='2' style='background-color:#17384c; color:white; text-align: center; line-height: 2'>{group.NameGroupES.ToUpper()}</td>";
        //            htmlContent += "</tr>";

        //            foreach (var questionResponse in group.questions)
        //            {
        //                countRowQuestion++;

        //                var backgroundRow = (countRowQuestion % 2 == 0) ? "#fcf8ec" : "#ffffff";
        //                htmlContent += "<tr style='background-color:" + backgroundRow + ";'>";
        //                htmlContent += $"<td style='padding: 8px 16px; width: 50%;white-space: pre-wrap'>{questionResponse.QuestionTextES.ToUpper()}</td>";
        //                htmlContent += $"<td style='padding: 8px 16px; width: 50%;white-space: pre-wrap;'>{questionResponse.ValueResponse.ToUpper()}</td>";
        //                htmlContent += "</tr>";
        //            }
        //        }

        //        htmlContent += "</tbody>";
        //        htmlContent += "</table>";
        //        htmlContent += "</div>";

                

        //        // Cerrar el cuerpo del HTML
        //        htmlContent += "</body>";
        //        htmlContent += "</html>";

        //        var fileStream = new MemoryStream(GeneratePdfFromHtml(htmlContent));
        //        return fileStream;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}













        public static byte[] GeneratePdfFromHtml(string htmlcontent, string footerText = "", string HeaderContent = "")
        {


            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 15, Bottom = 15 },

            };

            var footer = InsertLineBreaks(footerText, 165);

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlcontent,
                UseExternalLinks = true,
                
                    

                WebSettings = { DefaultEncoding = "utf-8" },
                FooterSettings =
                {
                    FontName = "Arial",
                    FontSize = 7,
                    Left = footer,


                },
                HeaderSettings =
                {
    
                    FontName = "Arial",
                    FontSize = 8,
                    Left ="Página [page] de [toPage]",
                    Spacing = 2.812
                },

            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

           
            var pdfBytes = _converter.Convert(pdf);


            return pdfBytes;
        }

        //public MemoryStream GenerateExcelOfManyInspections(List<ResponseInspectionFormToReportDTO> data)
        //{
        //    try
        //    {
        //        string[] nameFormsWithoutPresentationType = { "GANCHOS" };
        //        using (XLWorkbook wsbook = new XLWorkbook())
        //        {
        //            var ws = wsbook.AddWorksheet("Reporte Inspecciones");
        //            var cabecera = GetHeaderRowOfReportInspecciones(ws, data);
        //            cabecera.Style.Font.SetBold(true);
        //            cabecera.AdjustToContents();


        //            //data
        //            var indexDataRow = 2;
        //            foreach (var item in data)
        //            {
        //                var rowData = ws.Row(indexDataRow);
        //                rowData.Cell(1).Value = item.ClientName.ToUpper();
        //                rowData.Cell(2).Value = item.CodeOrder.ToUpper();
        //                rowData.Cell(3).Value = item.NameInspectionForm.ToUpper();
        //                var indexDataCells = 4;
        //                if (!nameFormsWithoutPresentationType.Contains(item.NameInspectionForm.ToUpper().Trim()))
        //                {
        //                    rowData.Cell(4).Value = item.PresentationTypeName.ToUpper();
        //                    indexDataCells = 5;
        //                }



        //                foreach (var group in item.groups)
        //                {

        //                    foreach (var question in group.questions)
        //                    {
        //                        rowData.Cell(indexDataCells).Value = question.ValueResponse.ToUpper();

        //                        indexDataCells++;
        //                    }
        //                }
        //                indexDataRow++;
        //            }

        //            ws.Columns().AdjustToContents();



        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                wsbook.SaveAs(stream);
        //                return stream;
        //            }


        //        }

        //    } catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public IXLRow GetHeaderRowOfReportInspecciones(IXLWorksheet ws, List<ResponseInspectionFormToReportDTO> data)
        //{
        //    if (data == null || data.Count == 0)
        //    {
        //        throw new Exception("No se encontraron inspecciones en esas fechas");
        //    }
        //    string[] nameFormsWithoutPresentationType = { "GANCHOS" };
        //    var indexColumn = 4;
        //    var cabecera = ws.FirstRow();
        //    //esto no tiene index 0 tal parece
        //    cabecera.Cell(1).Value = "CLIENTE";
        //    cabecera.Cell(1).Style.Fill.BackgroundColor = XLColor.DarkMidnightBlue;
        //    cabecera.Cell(1).Style.Font.FontColor = XLColor.White;
        //    cabecera.Cell(2).Value = "ORDEN DE INSPECCIÓN";
        //    cabecera.Cell(2).Style.Fill.BackgroundColor = XLColor.DarkMidnightBlue;
        //    cabecera.Cell(2).Style.Font.FontColor = XLColor.White;
        //    cabecera.Cell(3).Value = "TIPO DE INSPECCIÓN";
        //    cabecera.Cell(3).Style.Fill.BackgroundColor = XLColor.DarkMidnightBlue;
        //    cabecera.Cell(3).Style.Font.FontColor = XLColor.White;


        //    if (!nameFormsWithoutPresentationType.Contains(data.FirstOrDefault().NameInspectionForm.ToUpper().Trim()))
        //    {
        //        cabecera.Cell(4).Value = "TIPO DE PRESENTACIÓN";
        //        cabecera.Cell(4).Style.Fill.BackgroundColor = XLColor.DarkMidnightBlue;
        //        cabecera.Cell(4).Style.Font.FontColor = XLColor.White;
        //        indexColumn = 5;
        //    }
            

        //    string[] listOfQuestionsThatIHaveToConcateTheGroupName = { "CORTADOS", "RECHAZADOS", "PROCESADOS" };

            
        //    foreach (var item in data)
        //    {
        //        foreach (var group in item.groups)
        //        {
        //            group.questions = group.questions.DistinctBy(x => x.QuestionTextES.ToUpper()).ToList();
        //            foreach (var question in group.questions)
        //            {
        //                cabecera.Cell(indexColumn).Value = listOfQuestionsThatIHaveToConcateTheGroupName.Contains(question.QuestionTextES.ToUpper()) ? $"{group.NameGroupES.ToUpper()} - {question.QuestionTextES.ToUpper()}" : question.QuestionTextES.ToUpper();
        //                cabecera.Cell(indexColumn).Style.Fill.BackgroundColor = XLColor.DarkMidnightBlue;
        //                cabecera.Cell(indexColumn).Style.Font.FontColor = XLColor.White;
        //                cabecera.AdjustToContents(indexColumn);
        //                indexColumn++;
                        
        //            }
        //        }
        //    }

        //    return cabecera;


        //}

        public static string InsertLineBreaks(string input, int maxCharactersPerLine)
        {
            string[] lines = input.Split('\n');
            StringBuilder result = new StringBuilder();

            foreach (string line in lines)
            {
                string[] words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var currentLine = new StringBuilder();
                int currentLineLength = 0;

                foreach (string word in words)
                {
                    if (currentLineLength + word.Length + 1 > maxCharactersPerLine)
                    {
                        result.AppendLine(currentLine.ToString().Trim());
                        currentLine.Clear();
                        currentLineLength = 0;
                    }

                    currentLine.Append(word + " ");
                    currentLineLength += word.Length + 1; // consideramos también el espacio entre palabras
                }

                result.AppendLine(currentLine.ToString().Trim());
            }

            return result.ToString().Trim();
        }
    }
}
