using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using DatabaseCRUD.DataModel.Interface;
using DatabaseCRUD.Data;
using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.Auxiliary;

namespace DatabaseCRUD.DataModel
{
    public class Album : IAlbum, INotifyPropertyChanged
    {
        public Album()
        {
            Songs = new ObservableCollection<Song>();
        }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateRelease { get; set; }
        [Required]
        [ForeignKey(nameof(Artist))]
        public string ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ObservableCollection<Song> Songs { get; set; }
        public override string ToString()
        {
            return Title;
        }
        [NotMapped]
        public string DateReleaseText
        {
            get => DateRelease.ToString();
        }
        public void CreateSong(Song newSong)
        {
            Songs.Add(newSong);
        }
        public void UpdateSong(Song updateSong)
        {
            var item = Songs.FirstOrDefault(i => i.Id == updateSong.Id);
            if (item != null)
            {
                Songs.Remove(item);
                Songs.Add(updateSong);
            }
        }
        public void DeleteSong(Song delSong)
        {
            Songs.Remove(delSong);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
