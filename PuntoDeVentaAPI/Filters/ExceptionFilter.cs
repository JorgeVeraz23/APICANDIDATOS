using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Net;

namespace PuntoDeVentaAPI.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        //private readonly ILogger<ExceptionFilter> logger;
        //private readonly ApplicationDbContext _context;
        //private readonly MessageInfoDTO _infoDTO = new();
        //private readonly IConfiguration configuration;

        //public ExceptionFilter(ILogger<ExceptionFilter> logger, ApplicationDbContext context, IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //    this.logger = logger;
        //    _context = context;
        //}

        //public override async Task OnExceptionAsync(ExceptionContext context)
        //{
        //    // Capturar información específica para los logs
        //    var logMessage = $"\n\n----------------------------ERROR-{context.ActionDescriptor.DisplayName}----------------------------------\n" +
        //                     $"Request ID: {Activity.Current?.Id}\n" +
        //                     $"Request TraceIdentifier: {context.HttpContext.TraceIdentifier}\n" +
        //                     $"Date: {DateTime.Now}\n" +
        //                     $"Controller: {context.ActionDescriptor.RouteValues["controller"]}\n" +
        //                     $"Endpoint: {context.ActionDescriptor.DisplayName}\n" +
        //                     $"Message: {context.Exception.Message}\n" +
        //                     $"StackTrace: {context.Exception.StackTrace}\n" +
        //                     $"InnerException: {context.Exception?.InnerException?.ToString() ?? ""}\n" +
        //                     "-------------------------------------------------------------------------\n" +
        //                     "-------------------------------------------------------------------------\n\n";

        //    // Registrar en el archivo de registro usando Serilog o cualquier otra configuración que utilices
        //    try
        //    {
        //        Logs log = new();
        //        log.RequestID = Activity.Current?.Id ?? string.Empty;
        //        log.RequestTraceIdentifier = context.HttpContext.TraceIdentifier;
        //        log.Fecha = DateTime.Now;
        //        log.Controller = context.ActionDescriptor?.RouteValues["controller"] ?? string.Empty;
        //        log.Endpoint = context.ActionDescriptor?.DisplayName ?? string.Empty;
        //        log.Message = context.Exception?.Message ?? string.Empty;
        //        log.StackTrace = context.Exception?.StackTrace ?? string.Empty;
        //        log.InnerException = context.Exception?.InnerException?.ToString() ?? "-";
        //        log.Ambiente = configuration["Ambiente"] ?? "-";
        //        log.Plataform = "API";
        //        await _context.Logs.AddAsync(log);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(logMessage);
        //        logger.LogError(ex.Message);
        //    }

        //    // Configurar la excepción en el contexto
        //    context.ExceptionHandled = true;
        //    // Responder con una vista de error
        //    _infoDTO.Message = context.Exception?.Message;
        //    _infoDTO.Detail = context.Exception?.Message;
        //    _infoDTO.Success = false;
        //    context.Result = new ObjectResult(_infoDTO)
        //    {
        //        StatusCode = (int)HttpStatusCode.BadRequest
        //    };

        //    base.OnException(context);
        //}
    }
}
