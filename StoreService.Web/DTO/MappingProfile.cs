using AutoMapper;
using StoreService.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<ProgramBook, ProgramBookDto>();
            CreateMap<ProgramBookDto, ProgramBook>();
            CreateMap<ProgramListing, ProgramListingDto>();
            CreateMap<ProgramListingDto, ProgramListing>();
            CreateMap<Topic, TopicDto>();
            CreateMap<TopicDto, Topic>();
        }
    }
}
