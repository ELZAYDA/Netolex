using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class DbApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbApplicationContext(DbContextOptions<DbApplicationContext> options)
          : base(options)
        { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<MovieDirector> MovieDirector { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//for identity
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbApplicationContext).Assembly);
        }
    }
}
