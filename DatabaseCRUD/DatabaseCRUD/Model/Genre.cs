using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DatabaseCRUD.Data.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using DatabaseCRUD.Data.Implementation;
using DatabaseCRUD.Data.Repositories;

namespace DatabaseCRUD.DataModel
{
    public class Genre : IGenre, INotifyPropertyChanged
    {
        private readonly IGenreRepository GenreRepository;

        public Genre()
        {
            ArtistGenres = new ObservableCollection<ArtistGenre>();
        }
        public Genre(IGenreRepository genreRepository)
        {
            GenreRepository = genreRepository;
            ArtistGenres = new ObservableCollection<ArtistGenre>();
        }
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ObservableCollection<ArtistGenre> ArtistGenres { get; set; }

        [NotMapped]
        public ObservableCollection<Genre> ListGenres
        {
            get => new ObservableCollection<Genre>(GenreRepository.GetAllGenres());
        }
        [NotMapped]
        public ObservableCollection<Artist> Artists
        {
            get => new ObservableCollection<Artist>(ArtistGenres.Select(u => u.Artist).Distinct().ToList());
        }
        [NotMapped]
        public Artist CurrentArtist { get; set; }
        public void CreateArtist(Artist newArtist)
        {
            if (ArtistGenres.FirstOrDefault(u => u.ArtistId == newArtist.Id) == null)
            {
                var newArtGen = new ArtistGenre() { Genre = this, GenreId = this.Id, ArtistId = newArtist.Id, Artist = newArtist };
                ArtistGenres.Add(newArtGen);
            }
            else
            {
                MessageBox.Show("Такой артист уже добавлен!", "Ошибка!");
                return;
            }
        }
        public void UpdateArtist(ArtistGenre updateArtist)
        {
            var item = ArtistGenres.FirstOrDefault(u => u.ArtistId == updateArtist.Artist.Id);
            if (item != null)
            {
                ArtistGenres.Remove(item);
                ArtistGenres.Add(updateArtist);
            }
        }
        public void DeleteArtist(Artist delArtist)
        {
            var delArtGen = new ArtistGenre() { Genre = this, GenreId = this.Id, ArtistId = delArtist.Id, Artist = delArtist };
            ArtistGenres.Remove(delArtGen);
        }
        public override string ToString()
        {
            return Name;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
