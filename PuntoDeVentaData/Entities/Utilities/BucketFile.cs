using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaData.Entities.Utilities
{
    public class BucketFile : CrudEntities
    {
        [Key]
        public long IdBucketFile { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string TempFileName { get; set; }
        public string UrlFile { get; set; }
        public string UrlKey { get; set; }
        public bool Sync { get; set; } = false;
    }
}
