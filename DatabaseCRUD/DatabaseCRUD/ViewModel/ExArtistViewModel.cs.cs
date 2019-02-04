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
using DatabaseCRUD.Data.Interface;
using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.DataModel.Interface;
using DatabaseCRUD.View;

namespace DatabaseCRUD.ViewModel
{
    public class ExArtistViewModel : BaseViewModel
    {
        protected Command saveChangeCommand;
        public Artist Artist { get; set; }
        public ObservableCollection<Artist> Artists
        {
            get => Artist.ListArtist;
        }
        public Artist SelectedArtist { get; set; }
        public Action CloseAction { get; set; }
        public ExArtistViewModel(Artist artist)
        {
            Artist = artist;
        }
        public Command AddArtistCommand
        {
            get
            {
                return createItemCommand ??
                  (createItemCommand = new Command((selectedItem) =>
                  {
                      if (SelectedArtist.Id != null)
                      {
                          SelectArtist(SelectedArtist);
                          CloseAction?.Invoke();
                      }
                      else
                          MessageBox.Show("Выберите артиста!");
                  }));
            }
        }

        void SelectArtist(Artist selectedArtist)
        {
            Artist.Id = selectedArtist.Id;
            Artist.Name = selectedArtist.Name;
            Artist.Biography = selectedArtist.Biography;
            Artist.Country = selectedArtist.Country;
            Artist.Albums = selectedArtist.Albums;
            Artist.ArtistGenres = selectedArtist.ArtistGenres;
        }
    }
}
