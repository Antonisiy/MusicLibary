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
	public class AlbumRepository : IAlbumRepository
	{
		private DataContext context;
		public AlbumRepository(DataContext context)
		{
			this.context = context;	
		}
		public async Task Create(Album item)
		{
			await context.Albums.AddAsync(item);
            context.SaveChanges();
			return;
		}

		public async Task Delete(Album item)
		{
			context.Albums.Remove(item);
			await context.SaveChangesAsync();
			return;
		}

		public async Task<Album> GetItem(string id)
		{
			var res = await context.Albums.FindAsync(id);
			return res;
		}
        public async Task Update(Album item)
		{
            context.Albums.Update(item);
			await context.SaveChangesAsync();
			return;
		}
        public ObservableCollection<Album> GetAllAlbums()
        {
            return new ObservableCollection<Album>(context.Albums.ToList());
        }
        public ObservableCollection<Artist> GetAllArtist()
        {
            return new ObservableCollection<Artist>(context.Artists.ToList());
        }
        
    }
}
