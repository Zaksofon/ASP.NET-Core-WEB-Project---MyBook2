using AutoMapper;
using MyBook2.Data.Models;
using MyBook2.Models.Book;
using MyBook2.Models.Home;
using MyBook2.Services.Books;
using MyBook2.Services.Books.Models;

namespace MyBook2.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, BookGenreServiceModel>();

            CreateMap<Book, LatestBookServiceModel>();
            CreateMap<BookDetailsServiceModel, BookFormModel>();

            CreateMap<Book, BookServiceModel>()
                .ForMember(b => b.GenreName, cfg => cfg.MapFrom(b => b.Genre.Name));

            CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.UserId, cfg => cfg.MapFrom(b => b.Librarian.UserId))
                .ForMember(b => b.GenreName, cfg => cfg.MapFrom(b => b.Genre.Name));
        }
    }
}
