using AutoMapper;

using PuntoDeVentaAPI.Services;
var builder = WebApplication.CreateBuilder(args);
// Configure Kestrel
/*builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 104857600; // 100MB
    //options.Limits.RequestBody = 104857600; // 100MB
});*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);
var app = builder.Build();
//HangfireService? hangfireService = app.Services.GetService(typeof(HangfireService)) as HangfireService;
ILogger<Startup>? servicioLogger = app.Services.GetService(typeof(ILogger<Startup>)) as ILogger<Startup>;
//startup.Configure(app, app.Environment, servicioLogger, hangfireService);
startup.Configure(app, app.Environment, servicioLogger);
app.Run();
