using AutoMapper;
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
            });
            return mappingConfig;
        }
    }
}
