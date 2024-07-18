using Data.Dto.PartidoPoliticoDTO;
using Data.Dto.RedSocialDTO;
using Data.Entities.PartidosPoliticos;
using Data.Entities.RedSocial;
using Data.Interfaces.ApiCandidatosInterfaces;
using Data.Interfaces.SecurityInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.APICandidatosRepository
{
    public class RedSocialRepository : RedSocialInterface
    {

        private readonly ApplicationDbContext _context;

        private readonly IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO infoDTO = new MessageInfoDTO();
        private readonly string _username;
        private readonly string _ip;


        public RedSocialRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;

            _ip = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            _username = Task.Run(async () =>
            (
                await userManager.FindByNameAsync(
                    httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""
                )
            )?.UserName ?? "Desconocido").Result;
        }


        public async Task<MessageInfoDTO> Create(RedSocialDTO data)
        {
            var isAlreadyExist = await _context.RedSocials.Where(x => x.Active && x.Plataforma.ToUpper().Equals(data.Plataforma.ToUpper())).AnyAsync();

            if (isAlreadyExist)
            {
                infoDTO.AccionFallida("Ya existe una red social registrada con ese nombre", 400);
                return infoDTO;
            }

            RedSocial redSocial = new RedSocial();
            redSocial.Active = true;
            redSocial.Plataforma = data.Plataforma;
            redSocial.Url = data.Url;
            redSocial.IdCandidato = data.IdCandidato;
            redSocial.DateRegister = DateTime.Now;
            redSocial.UserRegister = _username;
            redSocial.IpRegister = _ip;

            await _context.RedSocials.AddAsync(redSocial);
            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("Se ha creado la red social");
            return infoDTO;
        }

        public async Task<MessageInfoDTO> Desactive(long id)
        {
            var redSocialToDelete = await _context.RedSocials.Where(x => x.Active && x.IdResocial == id).FirstOrDefaultAsync();
            if (redSocialToDelete != null)
            {
                infoDTO.AccionFallida("La red social seleccionado no se encuentra disponible", 400);
            }
            redSocialToDelete.DateDelete = DateTime.Now;
            redSocialToDelete.Active = false;
            redSocialToDelete.UserDelete = _username;
            redSocialToDelete.IpDelete = _ip;

            await _unit.SaveChangesAsync();

            infoDTO.AccionCompletada("La red social seleccionada a sido eliminado correctamente");

            return infoDTO;
        }

        public async Task<MessageInfoDTO> Edit(RedSocialDTO data)
        {
            try
            {
                var model = await _context.RedSocials.Where(x => x.Active && x.IdResocial == data.IdResocial).FirstOrDefaultAsync() ?? throw new Exception("No se encontro la red social");

                model.Plataforma = data.Plataforma;
                model.Url = data.Url;
                model.IdCandidato = data.IdCandidato;

                model.UserModification = _username;
                model.DateModification = DateTime.Now;
                model.IpModification = _ip;

                await _context.SaveChangesAsync();

                return infoDTO;
            }
            catch (Exception ex)
            {
                return infoDTO.ErrorInterno(ex, "RedSocialRepository", "Error al intentar actualizar la red social");
            }
        }

        public async Task<RedSocialDTO> Get(long id)
        {
            var redSocialSelected = await _context.RedSocials.Where(x => x.Active && x.IdResocial == id).Select(c => new RedSocialDTO
            {
                IdResocial = c.IdResocial,
                Plataforma = c.Plataforma,
                Url = c.Url,
                IdCandidato = c.IdCandidato,
            }
           ).FirstOrDefaultAsync();
            return redSocialSelected;
        }

        public async Task<List<MostrarRedSocialDTO>> GetAll()
        {
            var redSocial = await _context.RedSocials.Where(x => x.Active).Select(c => new MostrarRedSocialDTO
            {
                IdResocial = c.IdResocial,
                Plataforma = c.Plataforma,
                Url = c.Url,
                NombreCandidato = c.Candidato.NombreCandidato

            }).ToListAsync();
            return redSocial;
        }
    }
}
