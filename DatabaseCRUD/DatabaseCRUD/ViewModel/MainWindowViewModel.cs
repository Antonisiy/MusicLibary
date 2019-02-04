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
using DatabaseCRUD.DataModel;
using DatabaseCRUD.DataModel.Interface;
using DatabaseCRUD.View;

namespace DatabaseCRUD.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ArtistsModel _model;
        public Command createGenreCommand, selectionAlbumChangeCommand, selectionSongChangeCommand;
        public ObservableCollection<Artist> Artists
        {
            get => _model.Artists;
        }

        public MainWindowViewModel(ArtistsModel artistModel)
        {
            _model = artistModel;
        }

        public MainWindowViewModel()
        {
            
        }

        public Artist SelectedArtist { get; set; }
        public Album SelectedAlbum { get; set; }
        public Song SelectedSong { get; set; }
        public Command UpdateItemCommand
        {
            get
            {
                return updateItemCommand ??
                  (updateItemCommand = new Command((selectedItem) =>
                  {
                      if (SelectedArtist == null) return;
                      ArtistView addArtistWindow = new ArtistView(SelectedArtist);
                      if (addArtistWindow.ShowDialog() == true)
                      {
                          _model.UpdateArtist(SelectedArtist);
                      }
                  }));
            }
        }

        public Command CreateArtistCommand
        {
            get
            {
                return createItemCommand ??
                  (createItemCommand = new Command((obj) =>
                  {
                      SelectedArtist = new Artist();
                      ArtistView addArtistWindow = new ArtistView(SelectedArtist);
                      if (addArtistWindow.ShowDialog() == true)
                      {
                          _model.CreateArtist(SelectedArtist);
                      }
                  }));
            }
        }
        public Command CreateGenreCommand
        {
            get
            {
                return createGenreCommand ??
                  (createGenreCommand = new Command((obj) =>
                  {
                      ListGenres ListGenresWindow = new ListGenres();
                      if (ListGenresWindow.ShowDialog() == true)
                      {
                          
                      }
                  }));
            }
        }
        public Command DeleteItemCommand
        {
            get
            {
                return deleteItemCommand ??
                    (deleteItemCommand = new Command(obj =>
                    {
                        if (SelectedArtist == null) return;
                        MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Внимание!", MessageBoxButton.YesNo);
                        if (res.ToString() == "Yes")
                        {
                            _model.DeleteArtist(SelectedArtist);
                        }
                        else if (res.ToString() == "No")
                        {
                            deleteItemCommand = null;
                            return;
                        }
                    }, (obj) => Artists.Count() > 0));
            }

        }
        
        public Command SelectionAlbumChangeCommand
        {
            get
            {
                return selectionAlbumChangeCommand ??
                    (selectionAlbumChangeCommand = new Command(selectAlbum =>
                    {
                        var Album = selectAlbum as Album;
                        if (selectAlbum == null) return;
                        SelectedAlbum = Album;
                    }, (obj) => Artists.Count() > 0));
            }

        }
        public Command SelectionSongChangeCommand
        {
            get
            {
                return selectionAlbumChangeCommand ??
                    (selectionAlbumChangeCommand = new Command(selectSong =>
                    {
                        var Song = selectSong as Song;
                        if (selectSong == null) return;
                        SelectedSong = Song;
                    }, (obj) => Artists.Count() > 0));
            }

        }

    }
}
