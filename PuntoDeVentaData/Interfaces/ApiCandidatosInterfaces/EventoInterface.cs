using Data.Dto.CandidatoDTO;
using Data.Dto.EventoDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface EventoInterface
    {
        public Task<List<MostrarEventoDto>> GetAll();
        public Task<EventoDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(EventoDTO data);
        public Task<MessageInfoDTO> Edit(EventoDTO data);
    }
}
