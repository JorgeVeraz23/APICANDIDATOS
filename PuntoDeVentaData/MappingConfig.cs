using AutoMapper;
using Data.Dto.CandidatoDTO;
using Data.Dto.CargoDTO;
using Data.Dto.PartidoPoliticoDTO;
using Data.Dto.PropuestaDTO;
using Data.Entities.Candidatos;
using Data.Entities.Cargo;
using Data.Entities.PartidosPoliticos;
using Data.Entities.Propuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<GroupQuestionFormDTO, GroupQuestionForm>();
                //config.CreateMap<GroupQuestionForm, GroupQuestionFormDTO>();
                //config.CreateMap<QuestionsForm, QuestionFormDTO>();
                config.CreateMap<Candidato, MostrarCandidatoConDetalleDTO>()
               .ForMember(dest => dest.Propuestas, opt => opt.MapFrom(src => src.Propuestas));

                config.CreateMap<Propuesta, PropuestaDTO>();
                config.CreateMap<Cargo, CargoDTO>();
                config.CreateMap<Partido, PartidoDTO>();
            });
            return mappingConfig;
        }
    }
}
