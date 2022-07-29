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
    /// Логика взаимодействия для Doljnost.xaml
    /// </summary>
    public partial class Doljnost : Window
    {
        public Doljnost()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameText.Text) && !string.IsNullOrWhiteSpace(salaryText.Text))
            {
                string name = nameText.Text.Trim();
                decimal salary = -1;
                try
                {
                    salary = decimal.Parse(salaryText.Text);
                }
                catch
                {
                    MessageBox.Show("Неверный формат зарплаты");
                    return;
                }
                var positions = APIHelper.GET<List<Models.Doljnost>>("doljnosts");
                if (positions.Where(p => p.NameOfDolj == name).Count() == 0)
                {
                    APIHelper.POST("doljnosts", new Models.Doljnost(name, salary));
                    Close();
                }
                else MessageBox.Show("Такая должность уже существует");
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void salaryText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!"1234567890,".Contains(e.Text))
            {
                e.Handled = true;
                return;
            }
            if ((salaryText.Text.Contains(",") || salaryText.Text.Length == 0) && e.Text == ",") e.Handled = true;
        }
    }
}
