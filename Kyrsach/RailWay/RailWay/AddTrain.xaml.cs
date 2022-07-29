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
    /// Логика взаимодействия для AddTrain.xaml
    /// </summary>
    public partial class AddTrain : Window
    {
        public AddTrain()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTypeOfTrain addTypeOfTrain = new AddTypeOfTrain();
            addTypeOfTrain.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (typeBox.SelectedItem != null && !string.IsNullOrWhiteSpace(numberText.Text.Trim()))
            {
                APIHelper.POST<Train>("trains", new Train(int.Parse(typeBox.SelectedValue.ToString()), int.Parse(numberText.Text.Trim())));
                Close();
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            typeBox.ItemsSource = APIHelper.GET<List<TypeOfTrain>>("typeOfTrains");
            typeBox.DisplayMemberPath = "Name";
            typeBox.SelectedValuePath = "IdTypeOfTrain";
        }
    }
}
