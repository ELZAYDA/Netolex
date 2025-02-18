﻿using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using BLL.Repositories.Implementation;

namespace Netolex.Extentions
{
    public static class ApplicationServerExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IMovieRepo, MovieRepo>();
            //services.AddScoped<IGenreRepo, GenreRepo>();
            //services.AddScoped<IDirectorRepo, DirectorRepo>();
            //services.AddScoped<IActorRepo, ActorRepo>();
            //services.AddScoped<IMovieGenreRepo, MovieGenreRepo>();
            //services.AddScoped<IReviewRepo, ReviewRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
