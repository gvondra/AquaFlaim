using AquaFlaim.Forms.Framework;
using AquaFlaim.Interface.Forms.Models;
using AutoMapper;
namespace FormsAPI
{
    public sealed class MapperConfiguration
    {
        private static readonly AutoMapper.MapperConfiguration _mapperConfiguration;

        static MapperConfiguration()
        {
            _mapperConfiguration = new AutoMapper.MapperConfiguration(configExp =>
            {
                configExp.CreateMap<IFormQuestionType, FormQuestionType>();
                configExp.CreateMap<FormQuestionType, IFormQuestionType>();
                configExp.CreateMap<IFormSectionType, FormSectionType>();
                configExp.CreateMap<FormSectionType, IFormSectionType>();
                configExp.CreateMap<IFormType, FormType>();
                configExp.CreateMap<FormType, IFormType>();
            });
        }

        public static IMapper CreateMapper() => new Mapper(_mapperConfiguration);
    }
}
