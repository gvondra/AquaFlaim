using InterfaceModels = AquaFlaim.Interface.Log.Models;
using DataModels = AquaFlaim.Log.Data.Framework.Models;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LogAPI
{
    public class MapperConfiguration
    {
        private static AutoMapper.MapperConfiguration _mapperConfiguration;

        static MapperConfiguration()
        {
            _mapperConfiguration = new AutoMapper.MapperConfiguration(configExp =>
            {
                configExp.CreateMap<InterfaceModels.Exception, DataModels.ExceptionData>()
                .ForMember(e => e.Timestamp, opt => opt.NullSubstitute(DateTime.UtcNow))
                .ForMember(e => e.Timestamp, opt => opt.MapFrom((src, des) => ToUtc(src.Timestamp)))
                .ForMember(e => e.Data, opt => opt.MapFrom<string>((src, dest) => MapData(src.Data)));

                configExp.CreateMap<DataModels.MetricData, InterfaceModels.Metric>()
                .ForMember(e => e.Data, opt => opt.MapFrom<Dictionary<string, string>>((src, dest) => MapData(src.Data)));
                configExp.CreateMap<InterfaceModels.Metric, DataModels.MetricData>()
                .ForMember(e => e.Timestamp, opt => opt.NullSubstitute(DateTime.UtcNow))
                .ForMember(e => e.Timestamp, opt => opt.MapFrom((src, des) => ToUtc(src.Timestamp)))
                .ForMember(e => e.Data, opt => opt.MapFrom<string>((src, dest) => MapData(src.Data)));
                
                configExp.CreateMap<InterfaceModels.Trace, DataModels.TraceData>()
                .ForMember(e => e.Timestamp, opt => opt.NullSubstitute(DateTime.UtcNow))
                .ForMember(e => e.Timestamp, opt => opt.MapFrom((src, des) => ToUtc(src.Timestamp)))
                .ForMember(e => e.Data, opt => opt.MapFrom<string>((src, dest) => MapData(src.Data)));
            });
        }

        public static AutoMapper.MapperConfiguration GetMapperConfiguration() => _mapperConfiguration;

        public static IMapper CreateMapper() => new Mapper(_mapperConfiguration);

        private static string MapData(Dictionary<string, string> data)
        {
            if (data != null)
                return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = null });
            else
                return string.Empty;
        }

        private static Dictionary<string, string> MapData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            else
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(data, new JsonSerializerSettings { ContractResolver = null });
        }

        private static DateTime? ToUtc(DateTime? timestamp)
        {
            if (timestamp.HasValue)
                return timestamp.Value.ToUniversalTime();
            else
                return timestamp;
        }
    }
}
