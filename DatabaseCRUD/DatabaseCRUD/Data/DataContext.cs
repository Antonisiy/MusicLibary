using DatabaseCRUD.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;

namespace DatabaseCRUD.Data
{
    public class DataContext : DbContext
    {

        #region Properties
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        #endregion

        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MusicLibary;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ArtistGenre>()
            .HasKey(t => new { t.ArtistId, t.GenreId });

            builder.Entity<ArtistGenre>()
                .HasOne(sc => sc.Artist)
                .WithMany(s => s.ArtistGenres)
                .HasForeignKey(sc => sc.ArtistId);

            builder.Entity<ArtistGenre>()
                .HasOne(sc => sc.Genre)
                .WithMany(c => c.ArtistGenres)
                .HasForeignKey(sc => sc.GenreId);

            builder.Entity<Album>().HasOne(x => x.Artist).WithMany(x => x.Albums);

            builder.Entity<Song>().HasOne(x => x.Album).WithMany(x => x.Songs);

            Seed(builder);
        }

        private void Seed(ModelBuilder builder)
        {
            Random rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                builder.Entity<Genre>().HasData(new Genre
                {
                    Id = (i).ToString(),
                    Name = "Жанр" + rand.Next(10),
                });
            }

            for (int i = 0; i < 4; i++)
            {
                builder.Entity<Artist>().HasData(new Artist
                {
                    Id = (i).ToString(),
                    Name = "Артист" + rand.Next(10),
                });
            }

            for (int i = 0; i < 4; i++)
            {
                builder.Entity<ArtistGenre>().HasData(new ArtistGenre
                {
                    ArtistId = (i).ToString(),
                    GenreId = (i).ToString(),
                });
            }

            //for (int i = 0; i < 15; i++)
            //{
            //    builder.Entity<Album>().HasData(new Album
            //    {
            //        Id = (i).ToString(),
            //        ArtistId = (i%4).ToString(),
            //        Title = "Имя" + rand.Next(10),
            //    });
            //}

            //for (int i = 0; i < 150; i++)
            //{
            //    builder.Entity<Song>().HasData(new Song
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        AlbumId = (i % 15).ToString(),
            //        Title = "Название" + rand.Next(100),
            //    });
            //}
           
           
        }
    }
}