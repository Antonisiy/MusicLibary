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
using DatabaseCRUD.Data;
using DatabaseCRUD.DataModel;
using DatabaseCRUD.DataModel.Interface;

namespace DatabaseCRUD.View
{
    /// <summary>
    /// Логика взаимодействия для ArtistView.xaml
    /// </summary>
    public partial class ArtistView : Window
    {
        public ArtistView(Artist artist)
        {
            InitializeComponent();

            var vm = new ArtistViewModel(artist);
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
