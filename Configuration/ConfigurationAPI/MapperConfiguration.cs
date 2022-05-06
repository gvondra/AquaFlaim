using AquaFlaim.Config.Framework;
using AquaFlaim.Interface.Configuration.Models;
using AutoMapper;
namespace ConfigurationAPI
{
    public class MapperConfiguration
    {
        private static readonly AutoMapper.MapperConfiguration _mapperConfiguration;

        static MapperConfiguration()
        {
            _mapperConfiguration = new AutoMapper.MapperConfiguration(configExp =>
            {
                configExp.CreateMap<IItem, Item>();
                configExp.CreateMap<Item, IItem >();
                configExp.CreateMap<ILookup, Lookup>();
                configExp.CreateMap<Lookup, ILookup>();
            });
        }

        public static IMapper CreateMapper() => new Mapper(_mapperConfiguration);
    }
}
