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
    /// Логика взаимодействия для AddTypeOfTrain.xaml
    /// </summary>
    public partial class AddTypeOfTrain : Window
    {
        public AddTypeOfTrain()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(speedText.Text.Trim()) && !string.IsNullOrWhiteSpace(capacity.Text.Trim()) && !string.IsNullOrWhiteSpace(typeText.Text.Trim()))
            {
                APIHelper.POST("typeOfTrains", new TypeOfTrain(typeText.Text, speedText.Text, int.Parse(capacity.Text)));
                Close();
            }
            else MessageBox.Show("Заполните все поля");
        }
    }
}
