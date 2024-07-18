using Data.Dto.PropuestaDTO;
using Data.Dto.RedSocialDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface RedSocialInterface
    {

        public Task<List<MostrarRedSocialDTO>> GetAll();
        public Task<RedSocialDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(RedSocialDTO data);
        public Task<MessageInfoDTO> Edit(RedSocialDTO data);
    }
}
