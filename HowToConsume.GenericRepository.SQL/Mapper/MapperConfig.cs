using AutoMapper;
using HowToConsume.GenericRepository.SQL.Models.Entities;
using HowToConsume.GenericRepository.SQL.Models.Request;

namespace HowToConsume.GenericRepository.SQL.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<PersonCreateRequest, Person>();
            CreateMap<PersonUpdateRequest, Person>();
        }
    }
}
