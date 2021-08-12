using AutoMapper;
using System.Collections.Generic;

namespace PictureEngineer.Data.MappingProfiles
{
    public class HomeMapper : Profile
    {
        public HomeMapper()
        {
            CreateMap<Entities.Services, DTO.ServicesDto>();
            CreateMap<Entities.FAQs, DTO.FAQsDto>();
        }
    }
}