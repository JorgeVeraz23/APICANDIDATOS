using Data.Dto.TemplateDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.TemplateInterfaces
{
    public interface IEmailTemplate
    {
        public Task<MessageInfoDTO> InsertarPlantillaCorreo(EmailTemplate plantillaCorreo);
        public Task<List<ItemEmailTemplateDTO>> ListarPlantillaCorreo();
        public Task<ItemEmailTemplateDTO> BuscarPlantillaCorreo(string enumerador);
        public Task<MessageInfoDTO> ModificarPlantillaCorreo(EmailTemplate plantillaCorreo);
        public Task<MessageInfoDTO> EliminarPlantillaCorreo(string enumerador, string ip, string usuario);
    }
}
