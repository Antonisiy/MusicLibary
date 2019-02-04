using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DatabaseCRUD.DataModel
{
    public class GenresModel : INotifyPropertyChanged
    {
        private readonly IGenreRepository _genreRepository;
        public ObservableCollection<Genre> Genres { get; set; }
       
        public GenresModel(IGenreRepository genreRepository)
        {
            Genres = new ObservableCollection<Genre>(genreRepository.GetAllGenres());
            _genreRepository = genreRepository;
        }

        public void UpdateGenre(Genre updatedGenre)
        {
                _genreRepository.Update(updatedGenre);
        }

        public void CreateGenre(Genre newGenre)
        {
                _genreRepository.Create(newGenre);
                Genres.Add(newGenre);
        }

        public void DeleteGenre(Genre delGenre)
        {
            _genreRepository.Delete(delGenre);
            Genres.Remove(delGenre);
        }

      

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
