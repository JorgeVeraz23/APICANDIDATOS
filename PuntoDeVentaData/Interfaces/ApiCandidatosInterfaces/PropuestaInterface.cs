using Data.Dto.PocisionDTO;
using Data.Dto.PropuestaDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface PropuestaInterface
    {
        public Task<List<MostrarPropuestaDTO>> GetAll();
        public Task<PropuestaDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(PropuestaDTO data);
        public Task<MessageInfoDTO> Edit(PropuestaDTO data);

    }
}
