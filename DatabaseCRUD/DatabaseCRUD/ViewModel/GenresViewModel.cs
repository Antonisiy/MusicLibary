using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DatabaseCRUD.Auxiliary;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.View;

namespace DatabaseCRUD.ViewModel
{
    public class GenresViewModel : BaseViewModel
    {
        private readonly GenresModel _model;
        public Command saveChangeCommand;
        public Action CloseAction { get; set; }
        public ObservableCollection<Genre> Genres
        {
            get => _model.Genres;
        }

        public GenresViewModel(GenresModel genresModel)
        {
            _model = genresModel;
        }

        public GenresViewModel()
        {
            
        }
        public Genre SelectedGenre { get; set; }
        public Command SaveChangeCommand
        {
            get
            {
                return saveChangeCommand ??
                  (saveChangeCommand = new Command((selectedItem) =>
                  {
                      CloseAction?.Invoke();
                  }));
            }
        }
        public Command UpdateGenreCommand
        {
            get
            {
                return updateItemCommand ??
                  (updateItemCommand = new Command((selectedItem) =>
                  {
                      if (SelectedGenre == null) return;
                      GenreView addGenreWindow = new GenreView(SelectedGenre);
                      if (addGenreWindow.ShowDialog() == true)
                      {
                          _model.UpdateGenre(SelectedGenre);
                      }
                  }));
            }
        }

        public Command CreateGenreCommand
        {
            get
            {
                return createItemCommand ??
                  (createItemCommand = new Command((obj) =>
                  {
                      SelectedGenre = new Genre();
                      GenreView addGenreWindow = new GenreView(SelectedGenre);
                      if (addGenreWindow.ShowDialog() == true)
                      {
                          _model.CreateGenre(SelectedGenre);
                      }
                  }));
            }
        }
        public Command DeleteGenreCommand
        {
            get
            {
                return deleteItemCommand ??
                    (deleteItemCommand = new Command(obj =>
                    {
                        if (SelectedGenre == null) return;
                        MessageBoxResult res = MessageBox.Show("Вы уверены что хотите удалить элемент?", "Внимание!", MessageBoxButton.YesNo);
                        if (res.ToString() == "Yes")
                        {
                            _model.DeleteGenre(SelectedGenre);
                        }
                        else if (res.ToString() == "No")
                        {
                            deleteItemCommand = null;
                            return;
                        }
                    }, (obj) => Genres.Count() > 0));
            }

        }

    }
}
