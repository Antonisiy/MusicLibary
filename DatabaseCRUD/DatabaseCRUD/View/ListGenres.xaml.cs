using DatabaseCRUD.Auxiliary;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatabaseCRUD.View
{
    /// <summary>
    /// Логика взаимодействия для ListGenres.xaml
    /// </summary>
    public partial class ListGenres : Window
    {
        public ListGenres()
        {
            InitializeComponent();

            var vm = new GenresViewModel(IocKernel.Get<GenresModel>());
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() =>
                {
                    this.DialogResult = true;
                    this.Close();
                }
                    );
            DataContext = vm;
        }
    }
}
