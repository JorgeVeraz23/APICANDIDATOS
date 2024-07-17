using Data.Dto.ApoyoDTO;
using Data.Dto.CandidatoDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface CandidatoInterface
    {
        public Task<List<MostrarCandidatoDTO>> GetAll();
        public Task<CandidatoDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(CandidatoDTO data);
        public Task<MessageInfoDTO> Edit(CandidatoDTO data);
    }
}
