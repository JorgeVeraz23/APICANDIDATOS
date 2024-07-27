using Data.Dto.CandidatoDTO;
using Data.Dto.CargoDTO;
using Data.Dto.UtilitiesDTO;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.ApiCandidatosInterfaces
{
    public interface CargoInterface
    {
        public Task<List<CargoDTO>> GetAll();
        public Task<List<KeyValueDTO>> KeyValueCargo();
        public Task<CargoDTO> Get(long id);
        public Task<MessageInfoDTO> Desactive(long id);
        public Task<MessageInfoDTO> Create(CargoDTO data);
        public Task<MessageInfoDTO> Edit(CargoDTO data);
    }
}
