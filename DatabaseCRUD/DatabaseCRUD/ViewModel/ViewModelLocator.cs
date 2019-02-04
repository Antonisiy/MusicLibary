using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseCRUD.Auxiliary;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.ViewModel;

namespace DatabaseCRUD
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel
        {
            get { return IocKernel.Get<MainWindowViewModel>(); }
        }
        public GenresViewModel GenresViewModel
        {
            get { return IocKernel.Get<GenresViewModel>(); }
        }
    }
}
