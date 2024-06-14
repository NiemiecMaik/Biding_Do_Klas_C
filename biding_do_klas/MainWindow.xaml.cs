using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace biding_do_klas
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MediaItem> MediaCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MediaCollection = new ObservableCollection<MediaItem>();
            DataContext = this;
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            var newItem = new MediaItem();
            var editWindow = new EditMediaWindow(newItem);
            if (editWindow.ShowDialog() == true)
            {
                MediaCollection.Add(newItem);
            }
        }

        private void EditButton(object sender, RoutedEventArgs e)
        {
            if (MediaListBox.SelectedItem is MediaItem selectedItem)
            {
                var editWindow = new EditMediaWindow(selectedItem);
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wybierz element do edycji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (MediaListBox.SelectedItem is MediaItem selectedItem)
            {
                MediaCollection.Remove(selectedItem);
            }
            else
            {
                MessageBox.Show("Wybierz element do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ImportButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var jsonData = File.ReadAllText(openFileDialog.FileName);
                var importedItems = JsonConvert.DeserializeObject<ObservableCollection<MediaItem>>(jsonData);
                MediaCollection.Clear();
                foreach (var item in importedItems)
                {
                    MediaCollection.Add(item);
                }
            }
        }

        private void ExportButton(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                var jsonData = JsonConvert.SerializeObject(MediaCollection, Formatting.Indented);
                File.WriteAllText(saveFileDialog.FileName, jsonData);
            }
        }
    }

    public class MediaItem : INotifyPropertyChanged
    {
        private string _title;
        private string _creator;
        private string _publisher;
        private string _medium;
        private string _length;
        private DateTime _releaseDate;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Creator
        {
            get => _creator;
            set
            {
                _creator = value;
                OnPropertyChanged(nameof(Creator));
            }
        }

        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }

        public string Medium
        {
            get => _medium;
            set
            {
                _medium = value;
                OnPropertyChanged(nameof(Medium));
            }
        }

        public string Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                _releaseDate = value;
                OnPropertyChanged(nameof(ReleaseDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
