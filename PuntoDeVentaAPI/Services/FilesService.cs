using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AutoMapper;
using Azure.Identity;
using LinkQuality.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using Data;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Entities.Utilities;
using PuntoDeVentaData.Dto.UtilitiesDTO;

namespace LinkQuality.Api.Services
{
    public class FilesService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager _userManager;

        public FilesService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _configuration = configuration;
        }


        public FilesService(ApplicationDbContext context, IMapper mapper, IAmazonS3 s3Client, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _s3Client = s3Client;
            _configuration = configuration;
        }

        public FilesService(ApplicationDbContext context,
            IMapper mapper,
            IAmazonS3 s3Client,
            IConfiguration configuration,
            ApplicationUserManager userManager)
        {
            _context = context;
            _mapper = mapper;
            _s3Client = s3Client;
            _configuration = configuration;
            _userManager = userManager;
        }

        private async Task<bool> BucketExists(string bucketName)
        {
            return await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        }

        public string BuildKey(string fileName, bool modifyName = false)
        {
            var ambientBucket = string.IsNullOrEmpty(_configuration["Ambiente"]) ? "QA" : _configuration["Ambiente"];
            var date = DateTime.Now;
            var year = date.Year;
            var monthName = date.ToString("MMMM");
            string fileExtension = Path.GetExtension(fileName);

            fileName = Path.GetFileNameWithoutExtension(fileName).Trim();

            if (modifyName)
            {
                fileName = Guid.NewGuid().ToString();
                fileName = Regex.Replace(fileName, @"-", "");
            }

            var key = $"{ambientBucket}/{year}/{monthName}/{fileName}{fileExtension}";
            return key;
        }

        public async Task<bool> UploadFileFromWeb(IFormFile file, string key)
        {
            //Si Movil es true no se modifica el nombre para poder hacer la sincronizacion con las respectivas tablas 
            var bucketName = string.IsNullOrEmpty(_configuration["AWS:BucketName"])
                ? "banacheck"
                : _configuration["AWS:BucketName"];

            if (!await BucketExists(bucketName))
                throw new Exception($"Bucket {bucketName} no existe");

            if (file == null || file.Length == 0)
                throw new Exception("No se detecta ninguna archivo");

            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
            };

            await _s3Client.PutObjectAsync(request);
            return true;
        }

        public async Task<string> UploadFile(IFormFile file, bool Movil = false, string fileName = null)
        {
            //Si Movil es true no se modifica el nombre para poder hacer la sincronizacion con las respectivas tablas 
            var bucketName = string.IsNullOrEmpty(_configuration["AWS:BucketName"])
                ? "banacheck"
                : _configuration["AWS:BucketName"];

            var ambientBucket = string.IsNullOrEmpty(_configuration["Ambiente"])
                ? "QA"
                : _configuration["Ambiente"];

            if (!await BucketExists(bucketName))
                throw new Exception($"Bucket {bucketName} no existe");

            if (file == null || file.Length == 0)
                throw new Exception("No se detecta ninguna archivo");

            var date = DateTime.Now;
            var year = date.Year;
            var monthName = date.ToString("MMMM");

            if (!Movil)
            {
                fileName = Guid.NewGuid().ToString();
                fileName = Regex.Replace(fileName, @"-", "");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(file.FileName);
            }

            string fileExtension = Path.GetExtension(file.FileName);

            var key = $"{ambientBucket}/{year}/{monthName}/{fileName}{fileExtension}";

            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
            };

            await _s3Client.PutObjectAsync(request);
            return key;
        }

        //public async Task<List<ImageFromBucketDTO>> GetFilesUrlAccessOfInspection(long IdRegistrationFormInspection)
        //{
        //    var inspectionOfImage = await _context.RegistrationFormInspections
        //        .Where(x => x.Active && x.IdRegistrationFormInspection == IdRegistrationFormInspection)
        //        .FirstOrDefaultAsync() ?? throw new Exception("La inspección seleccionada ya no se encuentra disponible");

        //    var bucketName = _configuration["AWS:BucketName"] ?? "banacheck";

        //    if (!await BucketExists(bucketName))
        //        throw new Exception($"Bucket {bucketName} no existe");

        //    List<ImageFromBucketDTO> urlFiles = [];
        //    TimeSpan expiration = TimeSpan.FromMinutes(10);

        //    var allFilesOfInspection = await _context.RegistrationFormFiles
        //        .Where(x => x.Active && x.IdRegistrationFormInspection == inspectionOfImage.IdRegistrationFormInspection)
        //        .Include(b => b.BucketFile)
        //        .ToListAsync();

        //    if (allFilesOfInspection.Count == 0)
        //        throw new Exception("Inspección no cuenta con imágenes registradas");

        //    foreach (var file in allFilesOfInspection)
        //    {
        //        var urltoAdd = GeneratePreSignedURL(bucketName, file.BucketFile.UrlKey, expiration);
        //        urlFiles.Add(new ImageFromBucketDTO
        //        {
        //            IdFileBucket = file.IdBucketFile,
        //            IdRegistrationFormFile = file.IdRegistrationFormFile,
        //            UrlAccess = urltoAdd
        //        });
        //    }

        //    return urlFiles;
        //}

        //public async Task<List<string>> GetFilesUrlAccessOfInspectionBase64(long IdRegistrationFormInspection)
        //{
        //    var inspectionOfImage = await _context.RegistrationFormInspections
        //        .Where(x => x.Active && x.IdRegistrationFormInspection == IdRegistrationFormInspection).FirstOrDefaultAsync();
        //    if (inspectionOfImage == null)
        //    {
        //        throw new Exception("La inspección seleccionada ya no se encuentra disponible");
        //    }

        //    var bucketName = string.IsNullOrEmpty(_configuration["AWS:BucketName"])
        //        ? "banacheck"
        //        : _configuration["AWS:BucketName"];

        //    if (!await BucketExists(bucketName))
        //        throw new Exception($"Bucket {bucketName} no existe");

        //    List<string> urlFiles = new List<string>();
        //    TimeSpan expiration = TimeSpan.FromMinutes(10);
        //    var allFilesOfInspection = await _context.RegistrationFormFiles.Where(x => x.Active && x.IdRegistrationFormInspection == inspectionOfImage.IdRegistrationFormInspection).Include(b => b.BucketFile).ToListAsync();
        //    /*if (allFilesOfInspection.Count == 0)
        //        throw new Exception("Inspoección no cuenta con imágenes registradas");*/

        //    foreach( var file in allFilesOfInspection)
        //    {
        //        urlFiles.Add(await GetBase64FromImageUrlAsync(GeneratePreSignedURL(bucketName, file.BucketFile.UrlKey, expiration)));

        //    }

        //    return urlFiles;
        //}

        public string GeneratePreSignedURL(string bucketName, string objectKey, TimeSpan expiration)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                Expires = DateTime.UtcNow.Add(expiration),
                Protocol = Protocol.HTTPS
            };

            string url = _s3Client.GetPreSignedURL(request);
            return url;
        }


        public async Task<string> GetBase64FromImageUrlAsync(string imageUrl)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = await webClient.DownloadDataTaskAsync(imageUrl);
                    // Corregir la orientación de la imagen si es necesario
                    imageBytes = CorrectImageOrientation(imageBytes);
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                throw new Exception($"Error al obtener la imagen como Base64: {ex.Message}");
            }
        }


        private byte[] CorrectImageOrientation(byte[] imageBytes)
        {
            using (Image image = Image.Load(imageBytes))
            {
                if (image.Metadata.ExifProfile != null)
                {
                    // Buscar el valor de orientación en los metadatos EXIF
                    var orientationValue = image.Metadata.ExifProfile.Values.FirstOrDefault(x => x.Tag == ExifTag.Orientation);

                    if (orientationValue != null)
                    {
                        // Obtener la orientación de la imagen desde los metadatos EXIF
                        ushort orientation = (ushort)orientationValue.GetValue();

                        // Rotar la imagen según la orientación
                        switch (orientation)
                        {
                            case 2:
                                image.Mutate(x => x.Flip(FlipMode.Horizontal));
                                break;
                            case 3:
                                image.Mutate(x => x.Rotate(180));
                                break;
                            case 4:
                                image.Mutate(x => x.Rotate(180).Flip(FlipMode.Horizontal));
                                break;
                            case 5:
                                image.Mutate(x => x.Rotate(90).Flip(FlipMode.Horizontal));
                                break;
                            case 6:
                                image.Mutate(x => x.Rotate(90));
                                break;
                            case 7:
                                image.Mutate(x => x.Rotate(270).Flip(FlipMode.Horizontal));
                                break;
                            case 8:
                                image.Mutate(x => x.Rotate(270));
                                break;
                            default:
                                // No es necesario realizar ninguna corrección de orientación
                                break;
                        }
                    }
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                    return memoryStream.ToArray();
                }
            }
        }


        //FORMAT AND CLEAR BASE64
        #region FORMAT AND CLEAR BASE64
        private string FormatUtf8(string input)
        {
            byte[] byteArray = Encoding.Default.GetBytes(input);
            string utf8String = Encoding.UTF8.GetString(byteArray);
            return utf8String;
        }

        private string CleanBase64String(string base64String)
        {
            // Eliminar caracteres no válidos
            string cleanedBase64 = Regex.Replace(base64String, @"[^a-zA-Z0-9+/=]", "");
            // Eliminar espacios en blanco y saltos de línea
            cleanedBase64 = cleanedBase64.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
            // Asegurar longitud múltiplo de 4 y agregar padding '=' si es necesario
            int paddingLength = cleanedBase64.Length % 4;
            if (paddingLength > 0)
            {
                cleanedBase64 = cleanedBase64.PadRight(cleanedBase64.Length + (4 - paddingLength), '=');
            }
            // Verificar que la longitud sea múltiplo de 4
            if (cleanedBase64.Length % 4 != 0)
            {
                throw new Exception("Cadena Base64 no válida.");
            }
            return cleanedBase64;
        }
        static (string, string, string) ExtractContentTypeAndExtension(string base64String)
        {
            string[] parts = base64String.Split(',');
            if (parts.Length < 2)
            {
                throw new ArgumentException("El archivo de Base 64 proporcionado contine un formato inaválido!");
            }
            string contentTypePart = parts[0];
            string b64 = parts[1];
            string dataPart = parts[1];

            if (!contentTypePart.StartsWith("data:"))
            {
                throw new ArgumentException("El content type tiene un formato inválido!");
            }
            string[] contentTypeParts = contentTypePart.Split(';');
            if (contentTypeParts.Length < 2)
            {
                throw new ArgumentException("El content type tiene un formato inválido!");
            }
            string contentType = contentTypeParts[0].Substring(5); // Remove "data:"
            string extension = contentTypeParts[0].Split('/')[1]; // Get the second part after splitting by "/"

            return (contentType, extension, b64);
        }

        #endregion

        //public async Task<FileDTO> ProcessFileAsync(IFormFile file, FileDTO Data, string username, string ip)
        //{
        //    int counter = 1;
        //    try
        //    {
                
        //        var inspectionOfImage = await _context.RegistrationFormInspections.Where(x => x.Active && x.ReferencialRemote.Equals(Data.IdentifierCode)).FirstOrDefaultAsync();
        //        if(inspectionOfImage == null)
        //        {
        //            throw new Exception("La inspección seleccionada ya no se encuentra disponible. IdentifierCode: "+Data.IdentifierCode);
        //        }

        //        var s3BucketUrl = _configuration["AWS:S3BucketUrl"];
        //        Data.FileName = Data.FileName.Trim();
             
        //            using (Stream fileUploadStream = file.OpenReadStream())
        //            {
        //                string fileNameHeader = Path.GetFileName(file.FileName);
        //                Data.FileName = CambiarNombreArchivo(file, fileNameHeader);
        //                var fileExtension = Path.GetExtension(file.FileName);
        //                if (file == null) throw new Exception("No se enviaron archivos.");

        //                if (file != null && file.Length > 0)
        //                {
        //                // añadir un parametro true para no modificar el nombre del archivo
        //                var key = await UploadFile(file, true, Data.FileName);

        //                BucketFile Nuevo = new BucketFile();
        //                Nuevo.DateRegister = DateTime.Now;
        //                Nuevo.UserRegister = username;
        //                Nuevo.IpRegister = ip;
        //                Nuevo.Active = true;
        //                Nuevo.ContentType = file.ContentType;
        //                Nuevo.FileName = Data.FileName;
        //                Nuevo.TempFileName = fileNameHeader;
        //                Nuevo.UrlKey = key;
        //                Nuevo.UrlFile = key;
        //                await _context.BucketFiles.AddAsync(Nuevo);


        //                var newFileToInspection = new RegistrationFormFile();
        //                newFileToInspection.Active = true;
        //                newFileToInspection.DateRegister = DateTime.Now;
        //                newFileToInspection.UserRegister = username;
        //                newFileToInspection.IpRegister = ip;
        //                newFileToInspection.BucketFile = Nuevo;
        //                newFileToInspection.FileName = Data.FileName;
        //                newFileToInspection.RegistrationFormInspection = inspectionOfImage;
        //                await _context.RegistrationFormFiles.AddAsync(newFileToInspection);


        //                await _context.SaveChangesAsync();
                            
        //                }
        //            }
                
        //        return Data;
        //    }
        //    catch (Exception e)
        //    {
        //        //await DeleteFileAsync(Data.UrlKey);
        //        throw;
        //    }
        //}

        //public async Task<bool> SaveImageInspectionFromWeb(SaveImageInspectionFromWebDTO data, string username, string ip)
        //{
        //    var inspectionOfImage = await _context.RegistrationFormInspections.AsNoTracking()
        //        .Where(x =>
        //            x.Active &&
        //            x.IdRegistrationFormInspection == data.IdRegistrationFormInspection
        //        ).FirstOrDefaultAsync() ?? throw new Exception("La inspección seleccionada ya no se encuentra disponible.");

        //    var file = data.File;
        //    if (file == null && file?.Length == 0)
        //    {
        //        throw new Exception("No se detectó ningún archivo.");
        //    }

        //    string tempFileName = Path.GetFileName(file?.FileName ?? string.Empty).Trim();
        //    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file?.FileName ?? string.Empty).Trim();
        //    string ex = Path.GetExtension(file?.FileName ?? string.Empty);

        //    if (ex != ".jpeg" && ex != ".jpg" && ex != ".png")
        //    {
        //        throw new Exception("Se ha seleccionado un archivo no permitido.");
        //    }

        //    string newFileNameWithoutExtension = CambiarNombreArchivo(file, fileNameWithoutExtension);
        //    string newFileName = $"{newFileNameWithoutExtension}{ex}";
        //    string key = BuildKey(newFileName);

        //    try
        //    {
        //        var result = await UploadFileFromWeb(file, key);
        //        if (!result)
        //        {
        //            throw new Exception($"Falló al subir el archivo {tempFileName}");
        //        }

        //        var nuevo = new BucketFile
        //        {
        //            ContentType = file.ContentType,
        //            TempFileName = tempFileName,
        //            FileName = newFileName,
        //            UrlKey = key,
        //            UrlFile = key,

        //            Active = true,
        //            IpRegister = ip,
        //            UserRegister = username,
        //            DateRegister = DateTime.Now,
        //        };

        //        _context.BucketFiles.Add(nuevo);

        //        var newFileToInspection = new RegistrationFormFile
        //        {
        //            IdRegistrationFormInspection = inspectionOfImage.IdRegistrationFormInspection,
        //            BucketFile = nuevo,
        //            FileName = newFileName,

        //            Active = true,
        //            DateRegister = DateTime.Now,
        //            UserRegister = username,
        //            IpRegister = ip,
        //        };

        //        _context.RegistrationFormFiles.Add(newFileToInspection);
        //        await _context.SaveChangesAsync();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        await DeleteFileAsync(key);
        //        throw;
        //    }
        //}


        public async Task<bool> DeleteFileAsync(string key)
        {
            var bucketName = _configuration["AWS:BucketName"];
            if (!await BucketExists(bucketName))
                throw new Exception($"Bucket {bucketName} no existe");
            try
            {
                await _s3Client.DeleteObjectAsync(bucketName, key);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public async Task<bool> DeleteImageInspection(ImageFromBucketDTO data, string username, string ip)
        //{
        //    var model = await _context.RegistrationFormFiles
        //        .Where(x =>
        //            x.Active &&
        //            x.IdRegistrationFormFile == data.IdRegistrationFormFile
        //        ).FirstOrDefaultAsync() ?? throw new Exception("La imagen seleccionada ya no se encuentra disponible.");

        //    model.Active = false;
        //    model.UserDelete = username;
        //    model.DateDelete = DateTime.Now;
        //    model.IpModification = ip;

        //    await _context.SaveChangesAsync();
        //    return true;
        //}
        public string CambiarNombreArchivo(IFormFile file, string nombreArchivo)
        {
            var date = DateTime.Now;
            var defaultName = Path.GetFileName(file.FileName);
            var formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            var year = date.Year;
            var monthName = date.ToString("MMMM");
            var dayName = date.ToString("dd");
            var hour = date.ToString("HH");
            var min = date.ToString("mm");
            var seg = date.ToString("ss");
            var mls = date.Millisecond;
            var mcs = date.Microsecond;

            string fileExtension = Path.GetExtension(file.FileName);

            return $"{nombreArchivo}-{hour}:{min}:{seg}:{mls}:{mcs}";

        }

    }
}
