using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DatabaseCRUD.Data.Interface;
using DatabaseCRUD.Data.Repositories;
using System.Collections.ObjectModel;

namespace DatabaseCRUD.DataModel
{
    public class Song : ISong, INotifyPropertyChanged
    {
        public Song()
        {

        }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Duration { get; set; }
        [Required]
        [ForeignKey(nameof(Album))]
        public string AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public override string ToString()
        {
            return Title;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
