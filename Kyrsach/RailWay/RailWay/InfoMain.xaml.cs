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
    /// Логика взаимодействия для InfoMain.xaml
    /// </summary>
    public partial class InfoMain : Window
    {
        private List<string> formNames;
        private int roleId;

        public InfoMain()
        {
            InitializeComponent();
            roleId = 1;
        }

        public InfoMain(int roleId)
        {
            InitializeComponent();
            this.roleId = roleId;
        }

        private void EnterBTN(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            if (PageBox.SelectedItem != null)
            {
                switch ((string)PageBox.SelectedItem)
                {
                    case "Сотрудники": window = new AddStaff(); break;
                    case "Должности": window = new Doljnost(); break;
                    case "Бригады": window = new StaffOfTeam(); break;
                    case "Поезда": window = new AddTrain(); break;
                    case "Типы поездов": window = new AddTypeOfTrain(); break;
                    case "Расписание": window = new TimeTable(); break;
                    case "Маршруты": window = new AddRoute(); break;
                    case "Маршрут": window = new AddStop(); break;
                    case "Информация о пользователях": window = new UserView(); break;
                    case "Информация о сотрудниках": window = new Staff(); break;
                    case "Остановки": window = new FullCities();break;
                }
                window.Show();
            }
            else MessageBox.Show("Выберите страницу для перехода к ней");
        }

        private void ExitBTN(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (roleId)
            {
                case 1: //Добавление форм для роли админа
                    formNames = new List<string>
                    {
                        "Должности",
                        "Бригады",
                        "Поезда",
                        "Типы поездов",
                        "Расписание",
                        "Маршруты",
                        "Маршрут",
                        "Информация о пользователях",
                        "Информация о сотрудниках",
                        "Остановки"
                    }; break;
                case 2: //Добавление форм для роли менеджера
                    formNames = new List<string>
                    {
                        "Поезда",
                        "Бригады",
                        "Маршруты",
                        "Расписание",
                        "Информация о сотрудниках",
                        "Остановки"
                        //"",
                    }; break;
            }
            PageBox.ItemsSource = formNames;
        }
    }
}
