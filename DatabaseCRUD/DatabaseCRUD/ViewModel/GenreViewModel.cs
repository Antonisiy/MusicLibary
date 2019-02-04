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
    public class GenreViewModel : BaseViewModel
    {
        protected Command saveChangeCommand;
        public Genre Genre { get; set; }
        public ObservableCollection<ArtistGenre> Artists
        {
            get => Genre.ArtistGenres;
        }
        public ArtistGenre SelectedArtist { get; set; }
        public Action CloseAction { get; set; }
        public GenreViewModel(Genre genre)
        {
            Genre = genre;
        }
        public Command SaveChangeCommand
        {
            get
            {
                return saveChangeCommand ??
                  (saveChangeCommand = new Command((selectedItem) =>
                  {
                      if(Validate(Genre))
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

        public Command AddArtistCommand
        {
            get
            {
                return createItemCommand ??
                  (createItemCommand = new Command((selectedItem) =>
                  {
                      var Artist = IocKernel.Get<Artist>();
                      ExistentArtistView ExistentArtistWindow = new ExistentArtistView(Artist);
                      if (ExistentArtistWindow.ShowDialog() == true)
                      {
                          Genre.CreateArtist(Artist);
                      }
                  }));
            }
        }
        public Command UpdateArtistCommand
        {
            get
            {
                return updateItemCommand ??
                  (updateItemCommand = new Command((selectedItem) =>
                  {
                      if (SelectedArtist == null)
                          return;
                      ArtistView ArtistWindow = new ArtistView(SelectedArtist.Artist);
                      if (ArtistWindow.ShowDialog() == true)
                      {
                          Genre.UpdateArtist(SelectedArtist);
                      }
                  }));
            }
        }
        public Command DeleteArtistCommand
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
                            Genre.DeleteArtist(SelectedArtist.Artist);
                        }
                        else if (res.ToString() == "No")
                        {
                            deleteItemCommand = null;
                            return;
                        }
                    }, (obj) => Artists.Count() > 0));
            }

        }
        bool Validate(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
            {
                return false;
            }
            else
                return true;
        }
    }
}
