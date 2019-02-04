using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DatabaseCRUD.Auxiliary;
using System.Collections.ObjectModel;
using DatabaseCRUD.DataModel.Interface;
using System.ComponentModel.DataAnnotations;
using DatabaseCRUD.Data.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace DatabaseCRUD.DataModel
{
    public class Artist : IArtist, INotifyPropertyChanged
    {
        [NotMapped]
        public IArtistRepository ArtistRepository { get; set; }
        public Artist()
        {
            Albums = new ObservableCollection<Album>();
            ArtistGenres = new ObservableCollection<ArtistGenre>();
        }
        public Artist(IArtistRepository artistRepository)
        {
            ArtistRepository = artistRepository;
            Albums = new ObservableCollection<Album>();
            ArtistGenres = new ObservableCollection<ArtistGenre>();
        }
        [Required]
        public string Id { get; set; }
        public string name, biography, country;
        [Required]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Biography
        {
            get
            {
                return biography;
            }
            set
            {
                biography = value;
                OnPropertyChanged();
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ArtistGenre> artistGenres;
        public virtual ObservableCollection<ArtistGenre> ArtistGenres
        {
            get
            {
                return artistGenres;
            }
            set
            {
                artistGenres = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Album> albums;
        public virtual ObservableCollection<Album> Albums
        {
            get
            {
                return albums;
            }
            set
            {
                albums = value;
                OnPropertyChanged("Albums");
            }
        }

        [NotMapped]
        public ObservableCollection<Artist> ListArtist
        {
            get => new ObservableCollection<Artist>(ArtistRepository.GetAllArtists());
        }

        public override string ToString()
        {
            return Name;
        }
        public void CreateAlbum(Album newAlbum)
        {
            Albums.Add(newAlbum);

        }
        public void UpdateAlbum(Album updateAlbum)
        {

            var item = Albums.FirstOrDefault(u => u.Id == updateAlbum.Id);
            if (item != null)
            {
                Albums.Remove(item);
                Albums.Add(updateAlbum);
            }


        }
        public void DeleteAlbum(Album delAlbum)
        {
            Albums.Remove(delAlbum);
        }
        public void CreateGenre(Genre newGenre)
        {
            if (ArtistGenres.FirstOrDefault(u => u.GenreId == newGenre.Id) == null)
            {
                var newArtGen = new ArtistGenre() { Genre = newGenre, GenreId = newGenre.Id, ArtistId = this.Id, Artist = this };
                ArtistGenres.Add(newArtGen);
            }
            else
            {
                MessageBox.Show("Такой жанр уже добавлен!", "Ошибка!");
                return;
            }
        }
        public void UpdateGenre(ArtistGenre updateGenre)
        {
            var item = ArtistGenres.FirstOrDefault(u => u.GenreId == updateGenre.Genre.Id);
            if (item != null)
            {
                ArtistGenres.Remove(item);
                ArtistGenres.Add(updateGenre);
            }
        }
        public void DeleteGenre(Genre delGenre)
        {
            var delArtGen = new ArtistGenre() { Genre = delGenre, GenreId = delGenre.Id, ArtistId = this.Id, Artist = this };
            ArtistGenres.Remove(delArtGen);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
