using Data.Dto.PartidoPoliticoDTO;
using Data.Dto.PocisionDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface PocisionInterface
    {
        public Task<List<PocisionDTO>> GetAll();
        public Task<PocisionDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(PocisionDTO data);
        public Task<MessageInfoDTO> Edit(PocisionDTO data);
    }
}
