using Data.Dto.ApoyoDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface ApoyoInterface
    {
        public Task<List<MostrarApoyoDTO>> GetAll();
        public Task<ApoyoDTO> GetApoyo(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(ApoyoDTO data);
        public Task<MessageInfoDTO> Edit(ApoyoDTO data);
    }
}
