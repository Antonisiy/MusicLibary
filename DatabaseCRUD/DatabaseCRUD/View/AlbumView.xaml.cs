using DatabaseCRUD.DataModel;
using DatabaseCRUD.DataModel.Interface;
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
    /// Логика взаимодействия для AlbumView.xaml
    /// </summary>
    public partial class AlbumView : Window
    {
        public AlbumView(IAlbum album)
        {
            InitializeComponent();

            var vm = new AlbumViewModel(album);
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
