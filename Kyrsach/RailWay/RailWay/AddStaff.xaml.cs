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
    /// Логика взаимодействия для AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Window
    {
        private int EditId;
        private bool IsEdit => EditId != -1;
        public AddStaff()
        {
            InitializeComponent();
            EditId = -1;
        }

        public AddStaff(int id)
        {
            InitializeComponent();
            EditId = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameText.Text) || string.IsNullOrWhiteSpace(surnameText.Text) || genderBox.SelectedItem == null || doljnstBox.SelectedItem == null || snilsText.Text.Length != snilsText.MaxLength || InnText.Text.Length != InnText.MaxLength || seriapassText.Text.Length != seriapassText.MaxLength || numberpassText.Text.Length != numberpassText.MaxLength)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            staff newStaff = new staff(surnameText.Text, nameText.Text, firdnameText.Text, snilsText.Text, InnText.Text, seriapassText.Text, numberpassText.Text, genderBox.SelectedIndex == 0 ? true : false, int.Parse(doljnstBox.SelectedValue.ToString()));

            if (IsEdit)
            {
                newStaff.IdStaff = EditId;
                APIHelper.PUT("staffs", newStaff, newStaff.IdStaff);
            }
            else APIHelper.POST("staffs", newStaff);

            (Owner as Staff).RefreshGrid();
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

            var positions = APIHelper.GET<List<Models.Doljnost>>("doljnosts");
            doljnstBox.ItemsSource = positions;
            doljnstBox.DisplayMemberPath = "NameOfDolj";
            doljnstBox.SelectedValuePath = "IdDoljnost";

            if (IsEdit)
            {
                var staff = APIHelper.GET<staff>($"staffs/{EditId}");
                surnameText.Text = staff.Surname;
                nameText.Text = staff.Name;
                firdnameText.Text = staff.Firdname;
                doljnstBox.SelectedValue = staff.IdDoljnost;
                if (staff.Gender) genderBox.SelectedIndex = 0;
                else genderBox.SelectedIndex = 1;
                snilsText.Text = staff.Snils;
                InnText.Text = staff.INN;
                seriapassText.Text = staff.SeriaPass;
                numberpassText.Text = staff.NumberPass;
                addStaffBTN.Content = "Изменить";
            }
        }

        private void numberpassText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!"1234567890".Contains(e.Text))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
