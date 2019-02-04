using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseCRUD.Data;
using DatabaseCRUD.Data.Implementation;
using DatabaseCRUD.Data.Repositories;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.ViewModel;
using Ninject;
using Ninject.Modules;

namespace DatabaseCRUD.Auxiliary
{
    public static class IocKernel
    {
        private static StandardKernel _kernel;

        public static T Get<T>()
        {
            return _kernel.Get<T>();
            
        }

        public static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(modules);
            }
        }
    }

    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ArtistsModel>().ToSelf().InSingletonScope();
            Bind<GenresModel>().ToSelf().InSingletonScope();
            Bind<MainWindowViewModel>().ToSelf().InTransientScope();
            Bind<GenresViewModel>().ToSelf().InTransientScope();
            Bind<Album>().ToSelf();
            Bind<Genre>().ToSelf();
            Bind<IArtistRepository>().To<ArtistRepository>().InSingletonScope();
            Bind<IAlbumRepository>().To<AlbumRepository>().InSingletonScope(); 
            Bind<ISongRepository>().To<SongRepository>().InSingletonScope();
            Bind<IGenreRepository>().To<GenreRepository>().InSingletonScope();
            Bind<DataContext>().ToSelf().InSingletonScope();
        }
    }
}
