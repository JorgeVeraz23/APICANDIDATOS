using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Dto.UtilitiesDTO
{
    public class FileDTO
    {
        public string? FileName { get; set; }
        public string ExtensionFile { get; set; }
        public long? IdQuestionForm { get; set; }
        public string? UrlKey { get; set; }
        public IFormFile File { get; set; }
        //
        public string TempFileName { get; set; }
        public string IdentifierCode { get; set; }
    }

    public class SaveImageInspectionFromWebDTO
    {
        [JsonPropertyName("idRegistrationInspectionForm")]
        public long? IdRegistrationFormInspection { get; set; }

        [JsonPropertyName("idQuestionForm")]
        public long? IdQuestionForm { get; set; }

        [JsonPropertyName("file")]
        public required IFormFile File { get; set; }
    }

    public class ImageFromBucketDTO
    {
        public long IdFileBucket { get; set; }
        public long IdRegistrationFormFile { get; set; }
        public string? UrlAccess { get; set; }
    }

}
