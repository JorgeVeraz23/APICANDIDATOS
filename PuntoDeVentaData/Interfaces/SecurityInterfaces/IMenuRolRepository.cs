using PuntoDeVentaData.Dto.SecurityDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.SecurityInterfaces
{
    public interface IMenuRolRepository
    {
        public Task<List<MenuRolConsultaDto>> GetMenuPorRol(string IdRol);
        public Task<List<MainMenuRoleDTO>> GetMenuByRoleName(string RoleName);
        public Task<MessageInfoDTO> RegistrarMenuRol(MenuRole menurol);
        public Task<MessageInfoDTO> ModificarMenuRol(MenuRole menurol);
        public Task<MessageInfoDTO> EliminarMenuRole(ObjetoEliminacionDTO menurolEliminacion);

    }
}
