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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private int EditId;
        private bool IsEdit => EditId != -1;
        public AddUser()
        {
            InitializeComponent();
            EditId = -1;
        }

        public AddUser(int id)
        {
            InitializeComponent();
            EditId = id;
        }

        private void addUserBTN_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameText.Text) || string.IsNullOrWhiteSpace(surnameText.Text) || genderBox.SelectedItem == null || roleBox.SelectedItem == null || snilsText.Text.Length != snilsText.MaxLength || InnText.Text.Length != InnText.MaxLength || seriapassText.Text.Length != seriapassText.MaxLength || numberpassText.Text.Length != numberpassText.MaxLength || string.IsNullOrWhiteSpace(loginText.Text) || string.IsNullOrWhiteSpace(passwordText.Password))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (APIHelper.GET<List<User>>("users").Where(u => u.Login == loginText.Text).Count() != 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            User newUser = new User(surnameText.Text, nameText.Text, firdnameText.Text, snilsText.Text, InnText.Text, seriapassText.Text, numberpassText.Text, genderBox.SelectedIndex == 0 ? true : false, loginText.Text, passwordText.Password, int.Parse(roleBox.SelectedValue.ToString()));

            if (IsEdit)
            {
                newUser.IdUser = EditId;
                APIHelper.PUT("users", newUser, newUser.IdUser);
            }
            else APIHelper.POST("users", newUser);

            (Owner as UserView).RefreshGrid();
            Close();
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            genderBox.Items.Add("Мужской");
            genderBox.Items.Add("Женский");

            var roles = APIHelper.GET<List<Role>>("roles");
            roleBox.ItemsSource = roles;
            roleBox.DisplayMemberPath = "NameOfRole";
            roleBox.SelectedValuePath = "IdRole";

            if (IsEdit)
            {
                var user = APIHelper.GET<User>($"users/{EditId}");
                surnameText.Text = user.Surname;
                nameText.Text = user.Name;
                firdnameText.Text = user.Firdname;
                roleBox.SelectedValue = user.IdRole;
                if (user.Gender) genderBox.SelectedIndex = 0;
                else genderBox.SelectedIndex = 1;
                snilsText.Text = user.Snils;
                InnText.Text = user.INN;
                seriapassText.Text = user.SeriaPass;
                numberpassText.Text = user.NumberPass;
                loginText.Text = user.Login;
                passwordText.Password = user.Password;
                addUserBTN.Content = "Изменить";
            }
        }

        private void snilsText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!"1234567890".Contains(e.Text))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
