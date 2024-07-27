using Data.Interfaces.ApiCandidatosInterfaces;
using Data.Interfaces.CatalogsInterfaces;
using Data.Interfaces.SecurityInterfaces;
using Data.Interfaces.TemplateInterfaces;
using Data.Interfaces.UserInterfaces;
using Data.Repository.APICandidatosRepository;
using Data.Repository.CatalogsRepository;
using Data.Repository.TemplateRepository;
using Data.Repository.UtilitiesRepository;

using LinkQuality.Data.Repository.SecurityRepository;
using LinkQuality.Data.Repository.SeguridadRepository;
using LinkQuality.Data.Repository.UserRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DependencyContainer
    {
        public static IServiceCollection DependencyEF(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IdentityDbContext<ApplicationUser, ApplicationRole, string>, ApplicationDbContext>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuRolRepository, MenuRoleRepository>();
            
            services.AddScoped<IAuditoriaAccesosRepository, AuditoriaAccesosRepository>();
            services.AddScoped<IRolRepository, RolRepository>();

            services.AddScoped<IEmailTemplate, TemplateRepository>();

            services.AddScoped<UserInterface, UserRepository>();
            services.AddScoped<GeneralCatalogsInterface, GeneralCatalogsRepository>();

            #region API CANDIDATOS
            services.AddScoped<CandidatoInterface, CandidatoRepository>();
            services.AddScoped<PartidoInterface, PartidoRepository>();
            services.AddScoped<TrayectoriaInterface, TrayectoriaRepository>();
            services.AddScoped<TransparienciaInterface, TransparienciaRepository>();
            services.AddScoped<PropuestaInterface, PropuestaRepository>();
            services.AddScoped<EventoInterface, EventoRepository>();
            services.AddScoped<RedSocialInterface, RedSocialRepository>();
            services.AddScoped<CargoInterface, CargoRepository>();
            services.AddScoped<ApoyoInterface, ApoyoRepository>();
            services.AddScoped<PocisionInterface, PocisionRepository>();
            #endregion

            return services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}
