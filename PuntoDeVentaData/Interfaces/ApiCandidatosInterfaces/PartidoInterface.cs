using Data.Dto.EventoDTO;
using Data.Dto.PartidoPoliticoDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface PartidoInterface
    {
        public Task<List<PartidoDTO>> GetAll();
        public Task<PartidoDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(PartidoDTO data);
        public Task<MessageInfoDTO> Edit(PartidoDTO data);
    }
}
