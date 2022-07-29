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
    /// Логика взаимодействия для AddStop.xaml
    /// </summary>
    public partial class AddStop : Window
    {
        private class RouteToShow
        {
            public RouteToShow(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class StopToShow
        {
            public StopToShow(int idCity, string name, string platform, string time)
            {
                IdCity = idCity;
                Name = name;
                Platform = platform;
                Time = time;
            }

            public int IdCity { get; set; }
            public string Name { get; set; }
            public string Platform { get; set; }
            public string Time { get; set; }
        }

        private List<Route> routes;
        private List<City> cities;
        private int EditId;
        private bool IsEdit => EditId != -1;

        public AddStop()
        {
            InitializeComponent();
            EditId = -1;
        }

        public AddStop(int editId)
        {
            InitializeComponent();
            EditId = editId;
        }

        private void hourBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 1)
            {
                if ((sender as TextBox).Text[0] == '2' && !"0123".Contains(e.Text[0]))
                {
                    e.Handled = true;
                    return;
                }
            }
            else if (!"012".Contains(e.Text))
            {
                e.Handled = true;
                return;
            }
            if (!"1234567890".Contains(e.Text))
            {
                e.Handled = true;
                return;
            }
        }

        private void minutesBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!"012345".Contains(e.Text) && (sender as TextBox).Text.Length == 0)
            {
                e.Handled = true;
                return;
            }
            if (!"1234567890,".Contains(e.Text))
            {
                e.Handled = true;
                return;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            routes = APIHelper.GET<List<Route>>("routes");
            cities = APIHelper.GET<List<City>>("cities");
            var trains = APIHelper.GET<List<Train>>("trains");
            List<RouteToShow> routeShow = new List<RouteToShow>();
            foreach (Route route in routes)
            {
                routeShow.Add(new RouteToShow(route.IdRoute, $"{cities.Where(c => c.IdCity == route.IdCityDeparture).FirstOrDefault().Name}: П{route.PlatformDeparture} --> {cities.Where(c => c.IdCity == route.IdCityArrival).FirstOrDefault().Name}: П{route.PlatformArrival}"));
            }
            routeBox.ItemsSource = routeShow;
            routeBox.DisplayMemberPath = "Name";
            routeBox.SelectedValuePath = "Id";
            citystopBox.ItemsSource = cities;
            citystopBox.DisplayMemberPath = "Name";
            citystopBox.SelectedValuePath = "IdCity";
            trainBox.ItemsSource = trains;
            trainBox.DisplayMemberPath = "NumberOfTrain";
            trainBox.SelectedValuePath = "IdTrain";

            if (IsEdit)
            {
                var timeTable = APIHelper.GET<Models.TimeTable>($"timeTables/{EditId}");
                var stops = APIHelper.GET<List<Stop>>("stops");

                routeBox.SelectedValue = timeTable.IdRoute;
                trainBox.SelectedValue = timeTable.IdTrain;

                dateStart.SelectedDate = timeTable.DateTimeDeparted;
                hourBoxdeparture.Text = timeTable.DateTimeDeparted.Hour.ToString();
                minutesBoxdeparture.Text = timeTable.DateTimeDeparted.Minute.ToString();

                dateEnd.SelectedDate = timeTable.DateTimeArrived;
                hourBoxarrive.Text = timeTable.DateTimeArrived.Hour.ToString();
                minutesBoxarrive.Text = timeTable.DateTimeArrived.Minute.ToString();

                foreach(Stop stop in stops)
                {
                    if (stop.IdTimeTable == timeTable.IdTimeTable) stopsGrid.Items.Add(new StopToShow(stop.IdCity, cities.Where(c => c.IdCity == stop.IdCity).FirstOrDefault().Name, stop.Platform.ToString(), stop.TimeOfStop.ToString("HH:mm")));
                }

                label.Content = "Изменение расписания";
                addRouteBTN.Content = "Изменить расписание";
            }
        }

        private void addStopBTN_Click(object sender, RoutedEventArgs e)
        {
            if (routeBox.SelectedItem == null || citystopBox.SelectedItem == null || hourBox.Text.Length != 2 || minutesBox.Text.Length != 2 || platformBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            var selectedRoute = routes.Where(r => r.IdRoute == (int)routeBox.SelectedValue).FirstOrDefault();
            if (selectedRoute.IdCityArrival == (int)citystopBox.SelectedValue || selectedRoute.IdCityDeparture == (int)citystopBox.SelectedValue)
            {
                MessageBox.Show("Данный город уже есть в маршруте");
                return;
            }

            var dateStart = this.dateStart.SelectedDate.Value.AddHours(int.Parse(hourBoxdeparture.Text)).AddMinutes(int.Parse(minutesBoxdeparture.Text));
            var dateEnd = this.dateEnd.SelectedDate.Value.AddHours(int.Parse(hourBoxarrive.Text)).AddMinutes(int.Parse(minutesBoxarrive.Text));
            var dateStopCheck = dateStop.SelectedDate.Value.AddHours(int.Parse(hourBox.Text)).AddMinutes(int.Parse(minutesBox.Text));
            if (dateStopCheck <= dateStart || dateStopCheck >= dateEnd)
            {
                MessageBox.Show("Дата и время остановки не может быть больше даты прибытия или меньше даты отправления");
                return;
            }

            var stops = APIHelper.GET<List<Stop>>("stops");
            foreach (Stop stop in stops)
            {
                if (stop.IdCity == ((City)citystopBox.SelectedItem).IdCity && dateStopCheck < stop.TimeOfStop.AddMinutes(30) && dateStopCheck > stop.TimeOfStop.AddMinutes(-30) && stop.Platform == (int)platformBox.SelectedItem)
                {
                    MessageBox.Show("Данная платформа в это время занята");
                    return;
                }
            }

            var timeTables = APIHelper.GET<List<Models.TimeTable>>("timeTables");
            foreach (Models.TimeTable timeTable in timeTables)
            {
                var route = routes.Where(r => r.IdRoute == timeTable.IdRoute).FirstOrDefault();
                if (route.IdCityDeparture == ((City)citystopBox.SelectedItem).IdCity && dateStopCheck < timeTable.DateTimeDeparted.AddMinutes(30) && dateStopCheck > timeTable.DateTimeDeparted.AddMinutes(-30) && route.PlatformDeparture == (int)platformBox.SelectedItem)
                {
                    MessageBox.Show("Данная платформа в это время занята");
                    return;
                }
                if (route.IdCityArrival == ((City)citystopBox.SelectedItem).IdCity && dateStopCheck < timeTable.DateTimeArrived.AddMinutes(30) && dateStopCheck > timeTable.DateTimeArrived.AddMinutes(-30) && route.PlatformArrival == (int)platformBox.SelectedItem)
                {
                    MessageBox.Show("Данная платформа в это время занята");
                    return;
                }
            }

            foreach (StopToShow row in stopsGrid.Items)
            {
                if (row.IdCity == (int)citystopBox.SelectedValue)
                {
                    MessageBox.Show("Данный город уже есть в остановках");
                    return;
                }
                if (row.Time.Split(':')[0] == hourBox.Text && row.Time.Split(':')[1].Substring(0, 2) == minutesBox.Text && row.Time.Split(' ')[1] == dateStop.SelectedDate.Value.ToString("dd.MM.yyyy"))
                {
                    MessageBox.Show("На такое время уже обозначена остановка");
                    return;
                }
            }

            City newStop = (City)citystopBox.SelectedItem;
            stopsGrid.Items.Add(new StopToShow(newStop.IdCity, newStop.Name, ((int)platformBox.SelectedItem).ToString(), $"{hourBox.Text}:{minutesBox.Text} {dateStop.SelectedDate.Value.ToString("dd.MM.yyyy")}"));
        }

        private void deleteStopBTN_Click(object sender, RoutedEventArgs e)
        {
            if (stopsGrid.SelectedItem != null)
            {
                stopsGrid.Items.Remove(stopsGrid.SelectedItem);
            }
        }

        private void addTimeTableBTN_Click(object sender, RoutedEventArgs e)
        {
            if (routeBox.SelectedItem == null || hourBoxarrive.Text.Length != 2 || minutesBoxarrive.Text.Length != 2 || hourBoxdeparture.Text.Length != 2 || minutesBoxdeparture.Text.Length != 2)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            
            if (dateStart.SelectedDate > dateEnd.SelectedDate)
            {
                MessageBox.Show("Неверный формат дат");
                return;
            }

            DateTime dateS = dateStart.SelectedDate.Value;
            dateS = dateS.AddHours(int.Parse(hourBoxdeparture.Text));
            dateS = dateS.AddMinutes(int.Parse(minutesBoxdeparture.Text));

            DateTime dateE = dateEnd.SelectedDate.Value;
            dateE = dateE.AddHours(int.Parse(hourBoxarrive.Text));
            dateE = dateE.AddMinutes(int.Parse(minutesBoxarrive.Text));

            var stops = APIHelper.GET<List<Stop>>("stops");
            var route = routes.Where(r => r.IdRoute == (int)routeBox.SelectedValue).FirstOrDefault();
            foreach (Stop stop in stops)
            {
                if (stop.IdCity == route.IdCityDeparture && dateS < stop.TimeOfStop.AddMinutes(30) && dateS > stop.TimeOfStop.AddMinutes(-30) && stop.Platform == route.PlatformDeparture)
                {
                    MessageBox.Show("Платформа отбытия в это время занята");
                    return;
                }

                if (stop.IdCity == route.IdCityArrival && dateE < stop.TimeOfStop.AddMinutes(30) && dateE > stop.TimeOfStop.AddMinutes(-30) && stop.Platform == route.PlatformArrival)
                {
                    MessageBox.Show("Платформа прибытия в это время занята");
                    return;
                }
            }

            var timeTables = APIHelper.GET<List<Models.TimeTable>>("timeTables");
            foreach (Models.TimeTable timeT in timeTables)
            {
                var routeCheck = routes.Where(r => r.IdRoute == timeT.IdRoute).FirstOrDefault();
                if (routeCheck.IdCityDeparture == route.IdCityDeparture && dateS < timeT.DateTimeDeparted.AddMinutes(30) && dateS > timeT.DateTimeDeparted.AddMinutes(-30) && routeCheck.PlatformDeparture == route.PlatformDeparture)
                {
                    MessageBox.Show("Платформа отбытия в это время занята");
                    return;
                }
                if (routeCheck.IdCityArrival == route.IdCityDeparture && dateS < timeT.DateTimeArrived.AddMinutes(30) && dateS > timeT.DateTimeArrived.AddMinutes(-30) && routeCheck.PlatformArrival == route.PlatformDeparture)
                {
                    MessageBox.Show("Платформа отбытия в это время занята");
                    return;
                }

                if (routeCheck.IdCityDeparture == route.IdCityArrival && dateE < timeT.DateTimeDeparted.AddMinutes(30) && dateE > timeT.DateTimeDeparted.AddMinutes(-30) && routeCheck.PlatformDeparture == route.PlatformArrival)
                {
                    MessageBox.Show("Платформа прибытия в это время занята");
                    return;
                }
                if (routeCheck.IdCityArrival == route.IdCityArrival && dateE < timeT.DateTimeArrived.AddMinutes(30) && dateE > timeT.DateTimeArrived.AddMinutes(-30) && routeCheck.PlatformArrival == route.PlatformArrival)
                {
                    MessageBox.Show("Платформа прибытия в это время занята");
                    return;
                }
            }

            var timeTable = new Models.TimeTable(dateE, dateS, int.Parse(trainBox.SelectedValue.ToString()), int.Parse(routeBox.SelectedValue.ToString()));

            if (IsEdit)
            {
                timeTable.IdTimeTable = EditId;
                foreach(Stop stop in stops)
                {
                    if (stop.IdTimeTable == timeTable.IdTimeTable) APIHelper.DELETE("stops", stop, stop.IdStop);
                }
                APIHelper.PUT("timeTables", timeTable, timeTable.IdTimeTable);

                foreach (StopToShow stop in stopsGrid.Items)
                {
                    APIHelper.POST("stops", new Stop(stop.IdCity, timeTable.IdTimeTable, DateTime.Parse(stop.Time), int.Parse(stop.Platform)));
                }
            }
            else
            {
                var newTimeTable = APIHelper.POST("timeTables", timeTable);
                foreach (StopToShow stop in stopsGrid.Items)
                {
                    APIHelper.POST("stops", new Stop(stop.IdCity, newTimeTable.IdTimeTable, DateTime.Parse(stop.Time), int.Parse(stop.Platform)));
                }
            }
            (Owner as TimeTable).RefreshGrid();
            Close();
        }

        private void citystopBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (citystopBox.SelectedItem == null) return;

            var currentCity = (City)citystopBox.SelectedItem;
            platformBox.Items.Clear();
            for (int i = 1; i <= currentCity.PlatformCount; i++)
            {
                platformBox.Items.Add(i);
            }
            if (platformBox.Items.Count != 0) platformBox.SelectedIndex = 0;
        }
    }
}
