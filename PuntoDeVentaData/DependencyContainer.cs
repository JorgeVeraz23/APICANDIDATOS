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
            services.AddScoped<CandidatoInterface, CandidatoRepository>().Reverse();
            services.AddScoped<PartidoInterface, PartidoRepository>().Reverse();
            services.AddScoped<TrayectoriaInterface, TrayectoriaRepository>().Reverse();
            services.AddScoped<TransparienciaInterface, TransparienciaRepository>().Reverse();
            services.AddScoped<PropuestaInterface, PropuestaRepository>().Reverse();
            services.AddScoped<EventoInterface, EventoRepository>().Reverse();
            services.AddScoped<RedSocialInterface, RedSocialRepository>().Reverse();
            services.AddScoped<CargoInterface, CargoRepository>().Reverse();
            services.AddScoped<ApoyoInterface, ApoyoRepository>().Reverse();
            services.AddScoped<PocisionInterface, PocisionRepository>().Reverse();
            #endregion

            return services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}
