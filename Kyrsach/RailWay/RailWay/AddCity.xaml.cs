using RailWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddCity.xaml
    /// </summary>
    public partial class AddCity : Window
    {
        private int EditId;
        private bool IsEdit => EditId != -1;

        public AddCity()
        {
            InitializeComponent();
            EditId = -1;
        }

        public AddCity(int id)
        {
            InitializeComponent();
            EditId = id;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void numberText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(countBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (int.Parse(countBox.Text) == 0)
            {
                MessageBox.Show("У остановки должнен быть хотя бы один перрон");
                return;
            }

            City newCity = new City(nameBox.Text, int.Parse(countBox.Text));

            if (IsEdit)
            {
                newCity.IdCity = EditId;
                APIHelper.PUT("cities", newCity, newCity.IdCity);
            }
            else
            {
                APIHelper.POST("cities", newCity);
            }

            
            (Owner as FullCities).RefreshGrid();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                var city = APIHelper.GET<City>($"cities/{EditId}");
                nameBox.Text = city.Name;
                countBox.Text = city.PlatformCount.ToString();
                addBtn.Content = "Изменить";
            }
        }
    }
}
