using Data.Dto.ExperienciaDTO;
using Data.Dto.TransparienciaDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface TrayectoriaInterface
    {

        public Task<List<MostrarTrayectoriaDTO>> GetAll();
        public Task<TrayectoriaDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(TrayectoriaDTO data);
        public Task<MessageInfoDTO> Edit(TrayectoriaDTO data);

    }
}
