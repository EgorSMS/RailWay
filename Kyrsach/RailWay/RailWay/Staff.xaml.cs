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
    /// Логика взаимодействия для Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        private class StaffShow
        {
            public StaffShow(int id, string surname, string name, string thirdName, string position, string snils, string iNN, string seriaPass, string numberPass, string gender)
            {
                Id = id;
                Surname = surname;
                Name = name;
                ThirdName = thirdName;
                Position = position;
                Snils = snils;
                INN = iNN;
                SeriaPass = seriaPass;
                NumberPass = numberPass;
                Gender = gender;
            }

            public int Id { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string ThirdName { get; set; }
            public string Position { get; set; }
            public string Snils { get; set; }
            public string INN { get; set; }
            public string SeriaPass { get; set; }
            public string NumberPass { get; set; }
            public string Gender { get; set; }
        }

        public Staff()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        public void RefreshGrid()
        {
            staffGrid.Items.Clear();
            var staffs = APIHelper.GET<List<staff>>("staffs");
            var positions = APIHelper.GET<List<Models.Doljnost>>("doljnosts");
            foreach (staff staf in staffs)
            {
                staffGrid.Items.Add(new StaffShow(staf.IdStaff, staf.Surname, staf.Name, staf.Firdname, positions.Where(p => p.IdDoljnost == staf.IdDoljnost).FirstOrDefault().NameOfDolj, staf.Snils, staf.INN, staf.SeriaPass, staf.NumberPass, staf.Gender ? "Мужской" : "Женский"));
            }
        }

        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (staffGrid.SelectedItems.Count != 0)
            {
                var staffToDelete = APIHelper.GET<staff>($"staffs/{((StaffShow)staffGrid.SelectedItem).Id}");
                APIHelper.DELETE("staffs", staffToDelete, staffToDelete.IdStaff);
                RefreshGrid();
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AddStaff();
            window.Owner = this;
            window.Show();
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void staffGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (staffGrid.SelectedItem != null)
            {
                Window window = new AddStaff(((StaffShow)staffGrid.SelectedItem).Id);
                window.Owner = this;
                window.Show();
            }
        }
    }
}




