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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RailWay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void enterBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(loginBox.Text) && !string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                var users = APIHelper.GET<List<User>>("users");
                if (users.Where(u => u.Login == loginBox.Text.Trim() && u.Password == passwordBox.Password.Trim()).Count() != 0)
                {
                    var user = users.Where(u => u.Login == loginBox.Text.Trim() && u.Password == passwordBox.Password.Trim()).FirstOrDefault();
                    var window = new InfoMain(user.IdRole);
                    window.Show();
                    Hide();
                }
                else MessageBox.Show("Неправильный логин или пароль");
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
