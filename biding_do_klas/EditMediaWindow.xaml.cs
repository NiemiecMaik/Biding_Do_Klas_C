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

namespace biding_do_klas
{
    /// <summary>
    /// Logika interakcji dla klasy EditMediaWindow.xaml
    /// </summary>
    public partial class EditMediaWindow : Window
    {
        public MediaItem MediaItem { get; set; }

        public EditMediaWindow(MediaItem mediaItem)
        {
            InitializeComponent();
            MediaItem = mediaItem;
            DataContext = MediaItem;
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
