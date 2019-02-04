using DatabaseCRUD.Auxiliary;
using DatabaseCRUD.Data;
using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.DataModel.Interface;
using DatabaseCRUD.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseCRUD.ViewModel
{
    public class ArtistViewModel : BaseViewModel
    {
        protected Command saveChangeCommand, addAlbumCommand, updateAlbumCommand, deleteAlbumCommand, addGenreCommand, updateGenreCommand, deleteGenreCommand;

        public Artist Artist { get; set; }
        public ObservableCollection<Album> Albums
        {
            get => Artist.Albums;
        }
        public ObservableCollection<ArtistGenre> Genres
        {
            get => Artist.ArtistGenres;
        }
        public Album SelectedAlbum { get; set; }
        public ArtistGenre SelectedGenre { get; set; }
        public Action CloseAction { get; set; }
        public ArtistViewModel(Artist artist)
        {
            Artist = artist;
        }
        public Command SaveChangeCommand
        {
            get
            {
                return saveChangeCommand ??
                  (saveChangeCommand = new Command((selectedItem) =>
                  {
                      if (Validate(Artist))
                      {
                          CloseAction();
                      }
                      else
                      {
                          MessageBox.Show("Невалидные значения!", "Ошибка!");
                          saveChangeCommand = null;
                      }
                  }));
            }
        }

        public Command AddAlbumCommand
        {
            get
            {
                return addAlbumCommand ??
                  (addAlbumCommand = new Command((selectedItem) =>
                  {
                      SelectedAlbum = IocKernel.Get<Album>();
                      SelectedAlbum.Artist = Artist;
                      SelectedAlbum.ArtistId = Artist.Id;
                      AlbumView AlbumWindow = new AlbumView(SelectedAlbum);
                      if (AlbumWindow.ShowDialog() == true)
                      {
                          Artist.CreateAlbum(SelectedAlbum);
                      }
                  }));
            }
        }
        public Command UpdateAlbumCommand
        {
            get
            {
                return updateAlbumCommand ??
                  (updateAlbumCommand = new Command((selectedItem) =>
                  {
                      if (SelectedAlbum == null)
                          return;
                      SelectedAlbum.AlbumRepository = IocKernel.Get<IAlbumRepository>();
                      AlbumView AlbumWindow = new AlbumView(SelectedAlbum);
                      if (AlbumWindow.ShowDialog() == true)
                      {
                          Artist.UpdateAlbum(SelectedAlbum);
                      }
                  }));
            }
        }
        public Command DeleteAlbumCommand
        {
            get
            {
                return deleteAlbumCommand ??
                    (deleteAlbumCommand = new Command(obj =>
                    {
                        if (SelectedAlbum == null) return;
                        MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Внимание!", MessageBoxButton.YesNo);
                        if (res.ToString() == "Yes")
                        {
                            Artist.DeleteAlbum(SelectedAlbum);
                        }
                        else if (res.ToString() == "No")
                        {
                            deleteItemCommand = null;
                            return;
                        }
                    }, (obj) => Artist.Albums.Count() > 0));
            }

        }
        public Command AddGenreCommand
        {
            get
            {
                return addGenreCommand ??
                  (addGenreCommand = new Command((selectedItem) =>
                  {
                      var Genre = IocKernel.Get<Genre>();
                      ExistentGenreView GenreWindow = new ExistentGenreView(Genre);
                      if (GenreWindow.ShowDialog() == true)
                      {
                          Artist.CreateGenre(Genre);
                      }
                  }));
            }
        }
        public Command UpdateGenreCommand
        {
            get
            {
                return updateGenreCommand ??
                  (updateGenreCommand = new Command((selectedItem) =>
                  {
                      if (SelectedGenre == null)
                          return;
                      GenreView GenreWindow = new GenreView(SelectedGenre.Genre);
                      if (GenreWindow.ShowDialog() == true)
                      {
                          Artist.UpdateGenre(SelectedGenre);
                      }
                  }));
            }
        }
        public Command DeleteGenreCommand
        {
            get
            {
                return deleteGenreCommand ??
                    (deleteGenreCommand = new Command(obj =>
                    {
                        if (SelectedGenre == null) return;
                        MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Внимание!", MessageBoxButton.YesNo);
                        if (res.ToString() == "Yes")
                        {
                            Artist.DeleteGenre(SelectedGenre.Genre);
                        }
                        else if (res.ToString() == "No")
                        {
                            deleteItemCommand = null;
                            return;
                        }
                    }, (obj) => Artist.ArtistGenres.Count() > 0));
            }

        }
        bool Validate(Artist artist)
        {
            if (string.IsNullOrWhiteSpace(artist.Country) || string.IsNullOrWhiteSpace(artist.Name))
            {
                return false;
            }
            else
                return true;
        }
    }
}
