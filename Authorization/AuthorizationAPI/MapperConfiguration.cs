using AquaFlaim.Authorization.Framework;
using AquaFlaim.Interface.Authorization.Models;
using AutoMapper;
namespace AuthorizationAPI
{
    public sealed class MapperConfiguration
    {
        private static readonly AutoMapper.MapperConfiguration _mapperConfiguration;

        static MapperConfiguration()
        {
            _mapperConfiguration = new AutoMapper.MapperConfiguration(configExp =>
            {
                configExp.CreateMap<IRole, Role>();
                configExp.CreateMap<Role, IRole>();
            });
        }

        public static IMapper CreateMapper() => new Mapper(_mapperConfiguration);
    }
}
