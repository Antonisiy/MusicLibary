using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DatabaseCRUD.Auxiliary;
using DatabaseCRUD.Data;
using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.DataModel.Interface;
using DatabaseCRUD.View;

namespace DatabaseCRUD.ViewModel
{
    public class AlbumViewModel : BaseViewModel
    {
        protected Command saveChangeCommand;
        public Album Album { get; set; }
        public ObservableCollection<Artist> Artists
        {
            
            get {
                //для отображения в окне редактирования текущего артиста
                var Artists = Album.ListArtist;
                Artists.Add(Album.Artist);
                return Artists;
            }
        }
        public Artist SelectedArtist { get; set; }
        public ObservableCollection<Song> Songs
        {
            get => Album.Songs;
        }
        public Song SelectedSong { get; set; }
        public Action CloseAction { get; set; }
        public AlbumViewModel(IAlbum album)
        {
            Album = album as Album;
            SelectedArtist = Album.Artist;
        }
        public Command SaveChangeCommand
        {
            get
            {
                return saveChangeCommand ??
                  (saveChangeCommand = new Command((selectedItem) =>
                  {
                      if(Validate(Album))
                      {
                          Album.Artist = SelectedArtist;
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

        public Command AddSongCommand
        {
            get
            {
                return createItemCommand ??
                  (createItemCommand = new Command((selectedItem) =>
                  {
                      SelectedSong = IocKernel.Get<Song>();
                      SelectedSong.Album = Album;
                      SelectedSong.AlbumId = Album.Id;
                      SongView AlbumWindow = new SongView(SelectedSong);
                      if (AlbumWindow.ShowDialog() == true)
                      {
                          Album.CreateSong(SelectedSong);
                      }
                  }));
            }
        }
        public Command UpdateSongCommand
        {
            get
            {
                return updateItemCommand ??
                  (updateItemCommand = new Command((selectedItem) =>
                  {
                      if (SelectedSong == null)
                          return;
                      SelectedSong.SongRepository = IocKernel.Get<ISongRepository>();
                      SongView AlbumWindow = new SongView(SelectedSong);
                      if (AlbumWindow.ShowDialog() == true)
                      {
                          Album.UpdateSong(SelectedSong);
                      }
                  }));
            }
        }
        public Command DeleteSongCommand
        {
            get
            {
                return deleteItemCommand ??
                    (deleteItemCommand = new Command(obj =>
                    {
                        if (SelectedSong == null) return;
                        MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Внимание!", MessageBoxButton.YesNo);
                        if (res.ToString() == "Yes")
                        {
                            Album.DeleteSong(SelectedSong);
                        }
                        else if (res.ToString() == "No")
                        {
                            deleteItemCommand = null;
                            return;
                        }
                    }, (obj) => Album.Songs.Count() > 0));
            }

        }
        bool Validate(Album album)
        {
            if (string.IsNullOrWhiteSpace(album.Title) || (album.DateRelease > DateTime.Now) || (album.DateRelease < new DateTime(1900, 1, 1)))
            {
                return false;
            }
            else
                return true;
        }
    }
}
