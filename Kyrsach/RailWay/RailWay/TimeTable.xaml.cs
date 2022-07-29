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
    /// Логика взаимодействия для TimeTable.xaml
    /// </summary>
    public partial class TimeTable : Window
    {
        private class TimeTableShow
        {
            public TimeTableShow(int id, string dateDeparture, string dateArrival, string route, string stops)
            {
                Id = id;
                DateDeparture = dateDeparture;
                DateArrival = dateArrival;
                Route = route;
                Stops = stops;
            }

            public int Id { get; set; }
            public string DateDeparture { get; set; }
            public string DateArrival { get; set; }
            public string Route { get; set; }
            public string Stops { get; set; }
        }

        public TimeTable()
        {
            InitializeComponent();
        }

        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (timetableGrid.SelectedItem != null)
            {
                var timeTable = APIHelper.GET<Models.TimeTable>($"timeTables/{((TimeTableShow)timetableGrid.SelectedItem).Id}");
                var stops = APIHelper.GET<List<Stop>>("stops");

                foreach (Stop stop in stops)
                {
                    if (stop.IdTimeTable == timeTable.IdTimeTable) APIHelper.DELETE("stops", stop, stop.IdStop);
                }

                APIHelper.DELETE("timeTables", timeTable, timeTable.IdTimeTable);
                RefreshGrid();
            }
        }

        private void addrouteBTN_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AddStop();
            window.Owner = this;
            window.ShowDialog();
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        public void RefreshGrid()
        {
            timetableGrid.Items.Clear();

            var timeTables = APIHelper.GET<List<Models.TimeTable>>("timeTables");
            var cities = APIHelper.GET<List<City>>("cities");
            var routes = APIHelper.GET<List<Route>>("routes");
            var stops = APIHelper.GET<List<Stop>>("stops");
            
            foreach (Models.TimeTable timeTable in timeTables)
            {
                string stopsString = "";
                foreach (Stop stop in stops)
                {
                    if (stop.IdTimeTable == timeTable.IdTimeTable) stopsString += $"{cities.Where(c => c.IdCity == stop.IdCity).FirstOrDefault().Name} П{stop.Platform}: {stop.TimeOfStop.ToString("HH:mm")}; ";
                }

                var currentRoute = routes.Where(r => r.IdRoute == timeTable.IdRoute).FirstOrDefault();
                string routeString = $"{cities.Where(c => c.IdCity == currentRoute.IdCityDeparture).FirstOrDefault().Name}: П{currentRoute.PlatformDeparture} --> {cities.Where(c => c.IdCity == currentRoute.IdCityArrival).FirstOrDefault().Name}: П{currentRoute.PlatformArrival}";
                timetableGrid.Items.Add(new TimeTableShow(timeTable.IdTimeTable, timeTable.DateTimeDeparted.ToString("dd.MM.yyyy HH:mm"), timeTable.DateTimeArrived.ToString("dd.MM.yyyy HH:mm"), routeString, stopsString));
            }
        }

        private void timetableGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timetableGrid.SelectedItem != null)
            {
                Window window = new AddStop(((TimeTableShow)timetableGrid.SelectedItem).Id);
                window.Owner = this;
                window.ShowDialog();
            }
        }
    }
}
