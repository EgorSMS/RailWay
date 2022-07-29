using RailWay.Models;
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
    /// Логика взаимодействия для AddRoute.xaml
    /// </summary>
    public partial class AddRoute : Window
    {
        public AddRoute()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var cities = APIHelper.GET<List<City>>("cities");
            arrivedBox.ItemsSource = cities;
            arrivedBox.DisplayMemberPath = "Name";
            arrivedBox.SelectedValuePath = "IdCity";
            departureBox.ItemsSource = cities;
            departureBox.DisplayMemberPath = "Name";
            departureBox.SelectedValuePath = "IdCity";
            arrivedBox.SelectedIndex = 0;
            departureBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (arrivedBox.SelectedItem != null && departureBox.SelectedItem != null && departurePlatformBox.SelectedItem != null && arrivedPlatformBox.SelectedItem != null)
            {
                if ((City)arrivedBox.SelectedItem != (City)departureBox.SelectedItem)
                {
                    APIHelper.POST("routes", new Route(int.Parse(departureBox.SelectedValue.ToString()), int.Parse(arrivedBox.SelectedValue.ToString()), (int)departurePlatformBox.SelectedItem, (int)arrivedPlatformBox.SelectedItem));
                    Close();
                }
                else MessageBox.Show("Город прибытия и город отправления не может быть одинаковым");
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void arrivedBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (arrivedBox.SelectedItem == null) return;

            var currentCity = (City)arrivedBox.SelectedItem;
            arrivedPlatformBox.Items.Clear();
            for (int i = 1; i <= currentCity.PlatformCount; i++)
            {
                arrivedPlatformBox.Items.Add(i);
            }
            if (arrivedPlatformBox.Items.Count != 0) arrivedPlatformBox.SelectedIndex = 0;
        }

        private void departureBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (departureBox.SelectedItem == null) return;

            var currentCity = (City)departureBox.SelectedItem;
            departurePlatformBox.Items.Clear();
            for (int i = 1; i <= currentCity.PlatformCount; i++)
            {
                departurePlatformBox.Items.Add(i);
            }
            if (departurePlatformBox.Items.Count != 0) departurePlatformBox.SelectedIndex = 0;
        }
    }
}
