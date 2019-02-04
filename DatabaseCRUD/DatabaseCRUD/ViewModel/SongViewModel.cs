using DatabaseCRUD.Auxiliary;
using DatabaseCRUD.Data.Interface;
using DatabaseCRUD.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseCRUD.ViewModel
{
    public class SongViewModel : BaseViewModel
    {
        protected Command saveChangeCommand;
        public Song Song { get; set; }
        public Album SelectedAlbum { get; set; }
        public Action CloseAction { get; set; }
        public SongViewModel(ISong song)
        {
            Song = song as Song;
            SelectedAlbum = Song.Album;
        }
        public Command SaveChangeCommand
        {
            get
            {
                return saveChangeCommand ??
                  (saveChangeCommand = new Command((selectedItem) =>
                  {
                      if(Validate(Song))
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
        bool Validate(Song song)
        {
            if (string.IsNullOrWhiteSpace(song.Title) || string.IsNullOrWhiteSpace(song.Duration))
            {
                return false;
            }
            else
                return true;
        }

    }
}
