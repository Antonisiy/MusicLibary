using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD.Data.Implementation
{
	public class SongRepository : ISongRepository
	{
		private DataContext context;
		public SongRepository(DataContext context)
		{
			this.context = context;	
		}
		public async Task Create(Song item)
		{
			await context.Songs.AddAsync(item);
            context.SaveChanges();
			return;
		}

		public async Task Delete(Song item)
		{
			context.Songs.Remove(item);
			await context.SaveChangesAsync();
			return;
		}

		public async Task<Song> GetItem(string id)
		{
			var res = await context.Songs.FindAsync(id);
			return res;
		}
        public async Task Update(Song item)
		{
            context.Songs.Update(item);
			await context.SaveChangesAsync();
			return;
		}
        public ObservableCollection<Song> GetAllSongs()
        {
            return new ObservableCollection<Song>(context.Songs.ToList());
        }
        public ObservableCollection<Album> GetAllAlbums(string idArtist)
        {
            if(idArtist!=null)
            return new ObservableCollection<Album>(context.Artists.Find(idArtist)?.Albums.ToList());
            else
            return new ObservableCollection<Album>();
        }
    }
}
