using AutoMapper;
using WebApiNet6.Data;
using WebApiNet6.Models;

namespace WebApiNet6.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
