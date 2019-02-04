using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatabaseCRUD.DataModel
{
    public class ArtistsModel : INotifyPropertyChanged
    {
        private readonly IArtistRepository _artistRepository;
        public ObservableCollection<Artist> Artists { get; set; }

        public ArtistsModel(IArtistRepository artistRepository)
        {
            Artists = new ObservableCollection<Artist>(artistRepository.GetAllArtists());
            _artistRepository = artistRepository;
        }

        public void UpdateArtist(Artist updatedArtist)
        {
            _artistRepository.Update(updatedArtist);
        }

        public void CreateArtist(Artist newArtist)
        {
            _artistRepository.Create(newArtist);
            Artists.Add(newArtist);
        }

        public void DeleteArtist(Artist delArtist)
        {
            _artistRepository.Delete(delArtist);
            Artists.Remove(delArtist);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
