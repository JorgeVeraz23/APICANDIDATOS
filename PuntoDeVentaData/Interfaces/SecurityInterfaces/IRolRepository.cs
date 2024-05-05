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
    public interface IRolRepository
    {
        public Task<List<ItemRolDTO>> GetAllRoles();
        public Task<List<DropDownDTO>> GetAllRolesDrop();

        public Task<MessageInfoDTO> RegistrarRol(ApplicationRole rol);
        public Task<MessageInfoDTO> ModificarRol(RolDTO rolDTO);
        public Task<MessageInfoDTO> EliminarRol(ObjetoEliminacionStringDTO rol);
    }
}
