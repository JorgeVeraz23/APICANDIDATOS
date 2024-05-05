//using DocumentFormat.OpenXml.Office.CustomUI;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.SecurityInterfaces
{
    public interface IMenuRepository
    {
        public Task<List<Menu>> GetMenu();
        public Task<MessageInfoDTO> RegistrarMenu(Menu menu);
        public Task<MessageInfoDTO> ModificarMenu(Menu menu);
        public Task<MessageInfoDTO> EliminarMenu(ObjetoEliminacionDTO menuEliminacion);
    }
}
