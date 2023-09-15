using AutoMapper;
using MagicVilla_api.Modelos;
using MagicVilla_api.Modelos.Dto;

namespace MagicVilla_api
{
    public class MappingConfig: Profile
    {

        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
       
    }
}
