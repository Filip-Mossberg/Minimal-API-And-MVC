using AutoMapper;
using Labb1_Minimal_API.Models;
using Labb1_Minimal_API.Models.DTOs;

namespace Labb1_Minimal_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookCreateDTO>().ReverseMap();
            CreateMap<Book, BookUpdateDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
