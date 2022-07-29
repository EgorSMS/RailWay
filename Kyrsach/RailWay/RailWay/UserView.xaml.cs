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
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private class UserShow
        {
            public UserShow(int id, string surname, string name, string firdname, string idRole, string snils, string iNN, string seriaPass, string numberPass, string gender, string login, string password)
            {
                Id = id;
                Surname = surname;
                Name = name;
                Firdname = firdname;
                Snils = snils;
                INN = iNN;
                SeriaPass = seriaPass;
                NumberPass = numberPass;
                Login = login;
                Password = password;
                Role = idRole;
                Gender = gender;
            }
            public int Id { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Firdname { get; set; }
            public string Snils { get; set; }
            public string INN { get; set; }
            public string SeriaPass { get; set; }
            public string NumberPass { get; set; }
            public string Gender { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public UserView()
        {
            InitializeComponent();
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addUserBTN_Click_1(object sender, RoutedEventArgs e)
        {
            Window window = new AddUser();
            window.Owner = this;
            window.Show();
        }

        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.SelectedItem != null)
            {
                var user = APIHelper.GET<User>($"users/{((UserShow)userGrid.SelectedItem).Id}");
                APIHelper.DELETE("users", user, user.IdUser);
                RefreshGrid();
            }
        }

        private void userGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (userGrid.SelectedItem != null)
            {
                Window window = new AddUser(((UserShow)userGrid.SelectedItem).Id);
                window.Owner = this;
                window.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        public void RefreshGrid()
        {
            userGrid.Items.Clear();
            var users = APIHelper.GET<List<User>>("users");
            var roles = APIHelper.GET<List<Role>>("roles");
            foreach (User user in users)
            {
                userGrid.Items.Add(new UserShow(user.IdUser, user.Surname, user.Name, user.Firdname, roles.Where(r => r.IdRole == user.IdRole).FirstOrDefault().NameOfRole, user.Snils, user.INN, user.SeriaPass, user.NumberPass, user.Gender ? "Мужской" : "Женский", user.Login, user.Password));
            }
        }
    }
}
