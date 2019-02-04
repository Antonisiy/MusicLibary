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
    public class ExGenreViewModel : BaseViewModel
    {
        protected Command saveChangeCommand;
        public Genre Genre { get; set; }
        public ObservableCollection<Genre> Genres
        {
            get => Genre.ListGenres;
        }
        public Genre SelectedGenre { get; set; }
        public Action CloseAction { get; set; }
        public ExGenreViewModel(Genre genre)
        {
            Genre = genre;
        }
        public Command AddGenreCommand
        {
            get
            {
                return createItemCommand ??
                  (createItemCommand = new Command((selectedItem) =>
                  {
                      if (SelectedGenre!=null && SelectedGenre.Id != null )
                      {
                          SelectGenre(SelectedGenre);
                          CloseAction?.Invoke();
                      }
                      else
                          MessageBox.Show("Выберите жанр!");
                  }));
            }
        }

        void SelectGenre(Genre selectedGenre)
        {
            Genre.Id = selectedGenre.Id;
            Genre.Name = selectedGenre.Name;
            Genre.Description = selectedGenre.Description;
            Genre.ArtistGenres = selectedGenre.ArtistGenres;
        }
    }
}
