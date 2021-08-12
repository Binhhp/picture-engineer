using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PictureEngineer.Data.Domains;
using PictureEngineer.Data.DTO;
using PictureEngineer.Data.Entities;

namespace PictureEngineer.Data.MappingProfiles
{
   public class BlogsMapper : Profile
    {
        public BlogsMapper()
        {
            CreateMap<BlogDto, Blog>()
                .ForMember(
                    dest => dest.ImageName,
                    opt => opt.MapFrom(src => src.FileUpload.FileName));

            CreateMap<Blog, BlogDomain>();
        }
    }
}
