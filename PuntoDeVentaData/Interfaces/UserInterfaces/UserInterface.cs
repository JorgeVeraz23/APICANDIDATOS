using Data.Dto.GenericDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.UserInterfaces
{
    public interface UserInterface
    {
        public Task<List<GenericListDTO>> GetInspectorList();
        public Task<List<GenericListDTO>> GetActivesInspectorList();
        public Task<MessageInfoDTO> ActiveDesactiveInspectorSelected(string UserName);
        public Task<MessageInfoDTO> ValidateAppVersion(string AppVersion);

    }
}
