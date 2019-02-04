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
using System.Windows;

namespace DatabaseCRUD.Data.Implementation
{
	public class ArtistRepository : IArtistRepository
	{
		private DataContext context;
		public ArtistRepository(DataContext context)
		{
			this.context = context;	
		}
		public async Task Create(Artist item)
		{
                await context.Artists.AddAsync(item);
                context.SaveChanges();
                return;   
		}
        public async Task Delete(Artist item)
		{
			context.Artists.Remove(item);
            await context.SaveChangesAsync();
			return;
		}

		public async Task<Artist> GetItem(string id)
		{
			var res = await context.Artists.FindAsync(id);
			return res;
		}
        public async Task Update(Artist item)
		{
            context.Artists.Update(item);
            await context.SaveChangesAsync();
          
			
		}
        public ObservableCollection<Artist> GetAllArtists()
        {
            return new ObservableCollection<Artist>(context.Artists.ToList());
        }

    }
}
