using AutoMapper;
using MyBook2.Data.Models;
using MyBook2.Models.Book;
using MyBook2.Models.Home;
using MyBook2.Services.Books;

namespace MyBook2.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Book, LatestBookServiceModel>();
            this.CreateMap<BookDetailsServiceModel, BookFormModel>();

            this.CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.UserId, cfg => cfg.MapFrom(b => b.Librarian.UserId));
        }
    }
}
