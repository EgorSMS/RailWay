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
using RailWay.Models;

namespace RailWay
{
    /// <summary>
    /// Логика взаимодействия для StaffOfTeam.xaml
    /// </summary>
    public partial class StaffOfTeam : Window
    {
        private List<StaffShow> staffGrid = new List<StaffShow>();

        public StaffOfTeam()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var staffs = APIHelper.GET<List<staff>>("staffs");
            var positions = APIHelper.GET<List<Models.Doljnost>>("doljnosts");
            List<StaffShow> staffShow = new List<StaffShow>();
            foreach(staff staff in staffs)
            {
                staffShow.Add(new StaffShow(staff.IdStaff, $"{staff.Surname} {staff.Name} {staff.Firdname}, {positions.Where(p => p.IdDoljnost == staff.IdDoljnost).FirstOrDefault().NameOfDolj}", positions.Where(p => p.IdDoljnost == staff.IdDoljnost).FirstOrDefault().NameOfDolj));
            }
            employeeBox.ItemsSource = staffShow;
            employeeBox.DisplayMemberPath = "Name";
            employeeBox.SelectedValuePath = "Id";
            trainBox.ItemsSource = APIHelper.GET<List<Train>>("trains");
            trainBox.DisplayMemberPath = "NumberOfTrain";
            trainBox.SelectedValuePath = "IdTrain";
            RefreshDatagrid();
        }

        private void RefreshDatagrid()
        {
            brigadeGrid.ItemsSource = null;
            brigadeGrid.ItemsSource = staffGrid;
            brigadeGrid.Columns[0].Header = "Код";
            brigadeGrid.Columns[1].Header = "ФИО, Должность";
            brigadeGrid.Columns[2].Visibility = Visibility.Hidden;
            if (staffGrid.Count >= 2) plusButton.IsEnabled = false;
            else plusButton.IsEnabled = true;
            if (staffGrid.Count == 0) minusButton.IsEnabled = false;
            else minusButton.IsEnabled = true;
        }

        private class StaffShow
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }

            public StaffShow(int id, string name, string position)
            {
                this.Id = id;
                this.Name = name;
                this.Position = position;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (staffGrid.Contains((StaffShow)employeeBox.SelectedItem))
            {
                MessageBox.Show("Такой сотрудник уже есть в составе данной бригады");
                return;
            }
            if (staffGrid.Where(s => s.Position == ((StaffShow)employeeBox.SelectedItem).Position).Count() != 0)
            {
                MessageBox.Show("Сотрудник с такой должностью уже есть в данной бригаде");
                return;
            }
            staffGrid.Add((StaffShow)employeeBox.SelectedItem);
            RefreshDatagrid();
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            if (brigadeGrid.SelectedItem != null)
            {
                staffGrid.Remove((StaffShow)brigadeGrid.SelectedItem);
                RefreshDatagrid();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (staffGrid.Count != 2)
            {
                MessageBox.Show("В бригаде должно быть два сотрудника");
                return;
            }
            if (trainBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите поезд");
                return;
            }
            APIHelper.POST("staffOfTeams", new Models.StaffOfTeam(staffGrid[0].Id, staffGrid[1].Id, int.Parse(trainBox.SelectedValue.ToString())));
            Close();
        }
    }
}
