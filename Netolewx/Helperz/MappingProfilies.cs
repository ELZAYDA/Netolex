using AutoMapper;
using DAL.Models;
using Netolewx.ViewModels.ActorVM;
using Netolewx.ViewModels.DirectorVM;
using Netolewx.ViewModels.GenreVM;
using Netolewx.ViewModels.MovieVM;
using Netolewx.ViewModels.MovieVM.MovieVM;

namespace Netolewx.Helperz
{
    public class MappingProfilies:Profile
    {
        public MappingProfilies()
        {
            CreateMap<AddActorVM, Actor>();
            CreateMap<DetailsEditActorVM, Actor>().ReverseMap();

            CreateMap<AddMovieVM, Movie>();
            CreateMap<DetailsEditMovieVM, Movie>().ReverseMap();

            CreateMap<AddDirectorVM, Director>();
            CreateMap<DetailsEditDirectorVM, Director>().ReverseMap();

            CreateMap<AddGenreVM, Genre>();
            CreateMap<DetailsEditGenreVM, Genre>().ReverseMap();


        }
    }
}
