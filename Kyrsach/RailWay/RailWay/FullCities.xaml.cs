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

namespace RailWay
{
    /// <summary>
    /// Логика взаимодействия для FullCities.xaml
    /// </summary>
    public partial class FullCities : Window
    {
        private class CityShow
        {
            public CityShow(int id, string name, int count)
            {
                Id = id;
                Name = name;
                Count = count;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public int Count { get; set; }
        }

        public FullCities()
        {
            InitializeComponent();

        }

        private void exitBTN_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void RefreshGrid()
        {
            cityGrid.Items.Clear();
            var cities = APIHelper.GET<List<Models.City>>("cities");
            foreach (Models.City city in cities)
            {
                cityGrid.Items.Add(new CityShow(city.IdCity, city.Name, city.PlatformCount));
            }
        }

        private void cityGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (cityGrid.SelectedItem != null)
            {
                Window window = new AddCity(((CityShow)cityGrid.SelectedItem).Id);
                window.Owner = this;
                window.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void addDoljnostBTN_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AddCity();
            window.Owner = this;
            window.Show();
        }

        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (cityGrid.SelectedItems.Count != 0)
            {
                var cityToDelete = APIHelper.GET<Models.City>($"cities/{((CityShow)cityGrid.SelectedItem).Id}");
                APIHelper.DELETE("cities", cityToDelete, cityToDelete.IdCity);
                RefreshGrid();
            }
        }
    }
}
