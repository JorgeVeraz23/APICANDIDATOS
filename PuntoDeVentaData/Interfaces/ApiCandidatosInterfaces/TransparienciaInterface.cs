using Data.Dto.RedSocialDTO;
using Data.Dto.TransparienciaDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface TransparienciaInterface
    {
        public Task<List<MostrarTransparienciaDTO>> GetAll();
        public Task<TransparienciaDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(TransparienciaDTO data);
        public Task<MessageInfoDTO> Edit(TransparienciaDTO data);


    }
}
