
using LinkQuality.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json.Serialization;

using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LinkQuality.Api.Middlewares;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using static System.Net.WebRequestMethods;
using PuntoDeVentaAPI.Services;
using PuntoDeVentaData.Entities.Security;
using Data;
using PuntoDeVentaAPI.Filters;

namespace PuntoDeVentaAPI.Services
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string DataBaseName { get; } = string.Empty;

        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;

            // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(opciones =>
            {
                //Log para captar todos los exeptions no capturados
                opciones.Filters.Add(typeof(ExceptionFilter));// Agrega el filtro de excepciones personalizado
                //opciones.Conventions.Add(new SwaggerAgrupaPorVersion());
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // APPLICATION DB CONTEXT
            services.DependencyEF(Configuration);
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            /***************/
            /*Culture Info*/
            /***************/
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var esCultureInfo = new CultureInfo("es-EC");
                var enCultureInfo = new CultureInfo("en");
                //var srCultureInfo = new CultureInfo("sr");

                esCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                esCultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
                esCultureInfo.NumberFormat.PerMilleSymbol = ",";
                esCultureInfo.NumberFormat.CurrencyGroupSeparator = ";";

                var supportedCultures = new[]
                {
                                esCultureInfo,
                                enCultureInfo
                                //srCultureInfo
                 };

                options.DefaultRequestCulture = new RequestCulture(culture: enCultureInfo, uiCulture: esCultureInfo);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                            {
                                new QueryStringRequestCultureProvider(),
                                new CookieRequestCultureProvider()
                            };
            });

            /******************************************************************************/
            /*                                                                             */
            /*                                                                             */
            /*                                 LOGS                                        */
            /*                                                                             */
            /*                                                                             */
            /******************************************************************************/

            string projectName = Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;//GET PROYECT NAME

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                .WriteTo.File($"logs/{projectName}-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(Log.Logger);
            });

            // AWS - CONFIGURACION
            // Configurar las opciones de AWS desde appsettings.json
            var awsOptions = Configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(Configuration["AWS:AccessKey"], Configuration["AWS:SecretKey"]);
            services.AddDefaultAWSOptions(awsOptions);

            // Agregar el cliente de S3 al servicio
            services.AddAWSService<IAmazonS3>();

            // AUTENTICACIÓN JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTKey"] ?? string.Empty)),
                    ClockSkew = TimeSpan.Zero
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            // CACHE
            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LinqQuality.WebApi",
                    Version = "v1",
                    Description = "API RESTful para el sistema de navieras",
                    Contact = new OpenApiContact
                    {
                        Email = "soporte@apptelink.com",
                        Name = "Apptelink",
                        Url = new Uri("https://apptelink.com")
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                //var archivoXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var rutaXML = Path.Combine(AppContext.BaseDirectory, archivoXML);
                //c.IncludeXmlComments(rutaXML);
                //Omitir Endpoint que no pueden ser mapeados por swagger
                //c.DocInclusionPredicate((docName, apiDesc) => !new[] {
                //        (typeof(PruebasController), "Respuestas"),
                //       // (typeof(OtroControlador), "OtroMetodo")
                //      }.Any(t =>
                //      {
                //          var methodInfo = apiDesc.TryGetMethodInfo(out var info) ? info : null;
                //          return methodInfo != null && t.Item1 == methodInfo.DeclaringType && t.Item2 == methodInfo.Name;
                //      }));
            });
            services.AddAutoMapper(typeof(Startup));
            //services.AddScoped<MapperHelper>();
            services.AddDataProtection();
            services.AddAuthorization(opciones =>
            {
                opciones.AddPolicy("EsAdmin", politica => politica.RequireClaim("esAdmin"));
            });
            //Security on login
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3; // Número de intentos fallidos permitidos antes de bloquear la cuenta
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60); // Duración del bloqueo de cuenta
            });
            services.AddTransient<PuntoDeVentaAPI.Services.HashServices>();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "origen1",
                    app =>
                    {
                        app.WithOrigins([
                            "http://localhost:5173",
                            "http://localhost:3000",
                            "http://localhost:3001",
                            "https://qa.banacheck.apptelink.solutions", 
                            "https://banacheck.apptelink.solutions"
                        ])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                );
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(
            //        "AllowSpecificOrigin",
            //        builder =>
            //        {
            //            builder.WithOrigins(["http://localhost:3000", "https://qa.banacheck.apptelink.solutions", "https://banacheck.apptelink.solutions"])
            //                   .AllowAnyHeader()
            //                   .AllowAnyMethod();
            //        }
            //    );
            //});
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<ApplicationUserManager>();

            // IDENTITY (USER SYSTEM)
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddDataProtection();
        }

        /*public class DashboardNoAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext dashboardContext)
            {
                return true;
            }
        }*/

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup>? logger/*, HangfireService? hangfireService*/)
        {
            //Captar todas las peticiones en logs y en terminal
            app.UseLoginResponseHTTP();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Naviera.API v1");
            });
            app.UseRouting();

            //app.UseCors("AllowSpecificOrigin");

            //CORS es relevante para navegadores y proyectos hechos en react, angular, etc, para aplicaciones moviles y de mas no tiene sentido realizarlo 
            app.UseCors("origen1");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }
}
