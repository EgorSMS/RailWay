using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebRailwayApp.Models;

namespace WebRailwayApp.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private RailwayDBContext db;

        public HomeController(ILogger<HomeController> logger, RailwayDBContext context)
        {
            _logger = logger;
            this.db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(Cities city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                await db.SaveChangesAsync();
                return Redirect("InfoCity"); //ПОМЕНЯТЬ ВЫВОД НА СПИСОК ГОРОДОВ
            }
            return View(city);
        }

        public async Task<IActionResult> InfoCity()
        {
            return View(await db.Cities.ToListAsync());
        }

        public async Task<IActionResult> DeleteCity(int id)
        {
            var cityToDelete = await db.Cities.FirstAsync(s => s.ID_City == id);
            if (cityToDelete != null)
            {
                db.Cities.Remove(cityToDelete);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("InfoCity");
        }

        public async Task<IActionResult> AddRoute()
        {
            RouteDisplay routeDisplay = new RouteDisplay();
            routeDisplay.cities = await db.Cities.ToListAsync();
            return View(routeDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoute(RouteDisplay routeDisplay)
        {
            routeDisplay.cities = await db.Cities.ToListAsync();
            if (ModelState.IsValid)
            {
                routeDisplay.ErrorDeparturePlatform = false;
                routeDisplay.ErrorArrivePlatform = false;
                if (routeDisplay.route.PlatformDeparture > db.Cities.FirstOrDefaultAsync(c => c.ID_City == routeDisplay.route.ID_City_Departure).Result.PlatformCount)
                    routeDisplay.ErrorDeparturePlatform = true;
                if (routeDisplay.route.PlatformArrival > db.Cities.FirstOrDefaultAsync(c => c.ID_City == routeDisplay.route.ID_City_Arrival).Result.PlatformCount)
                    routeDisplay.ErrorArrivePlatform = true;

                if (routeDisplay.ErrorArrivePlatform || routeDisplay.ErrorDeparturePlatform) return View(routeDisplay);
                if (routeDisplay.route.ID_City_Arrival == routeDisplay.route.ID_City_Departure)
                    routeDisplay.ErrorCities = true;

                if (routeDisplay.ErrorArrivePlatform || routeDisplay.ErrorDeparturePlatform || routeDisplay.ErrorCities)
                    return View(routeDisplay);

                db.Route.Add(routeDisplay.route);
                await db.SaveChangesAsync();
                return Redirect("Index"); 
            }
            return View(routeDisplay);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Authorization", "Auth");
        }

        public async Task<IActionResult> AddStaff()
        {
            staffDisplay staffDisplay = new staffDisplay();
            staffDisplay.doljnosts = await db.Doljnost.ToListAsync();
            return View(staffDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(staffDisplay staffDisplay)
        {
            staffDisplay.doljnosts = await db.Doljnost.ToListAsync();
            if (ModelState.IsValid)
            {
                db.staff.Add(staffDisplay.staff);
                await db.SaveChangesAsync();
                return RedirectToAction("InfoStaff");
            }
            return View(staffDisplay);
        }

        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staffToDelete = await db.staff.FirstAsync(s => s.ID_Staff == id);
            if (staffToDelete != null)
            {
                db.staff.Remove(staffToDelete);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("InfoStaff");
        }

        public async Task<IActionResult> InfoStaff()
        {
            staffDisplay staffDisplay = new staffDisplay();
            staffDisplay.staffs = await db.staff.ToListAsync();
            staffDisplay.doljnosts = await db.Doljnost.ToListAsync();
            return View(staffDisplay);
        }

        public async Task<IActionResult> AddTimeTable()
        {
            TimeTableDisplay timeTableDisplay = new TimeTableDisplay();
            timeTableDisplay.cities = await db.Cities.ToListAsync();
            timeTableDisplay.trains = await db.Train.ToListAsync();
            timeTableDisplay.routes = await db.Route.ToListAsync();
            return View(timeTableDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeTable(TimeTableDisplay timeTableDisplay)
        {
            timeTableDisplay.cities = await db.Cities.ToListAsync();
            timeTableDisplay.trains = await db.Train.ToListAsync();
            var routes = await db.Route.ToListAsync();
            timeTableDisplay.routes = routes;
            if (ModelState.IsValid)
            {
                if (timeTableDisplay.timeTable.DateTimeArrived <= timeTableDisplay.timeTable.DateTimeDeparted) 
                    timeTableDisplay.ErrorDate = true;

                DateTime dateS = timeTableDisplay.timeTable.DateTimeDeparted;
                DateTime dateE = timeTableDisplay.timeTable.DateTimeArrived;

                var stops = await db.Stops.ToListAsync();
                var route = routes.FirstOrDefault(r => r.ID_Route == timeTableDisplay.timeTable.ID_Route);
                foreach (Stops stop in stops)
                {
                    if (stop.ID_City == route.ID_City_Departure && dateS < stop.TimeOfStop.AddMinutes(30) && dateS > stop.TimeOfStop.AddMinutes(-30) && stop.Platform == route.PlatformDeparture)
                        timeTableDisplay.ErrorDateDepartureAlreadyExist = true;

                    if (stop.ID_City == route.ID_City_Arrival && dateE < stop.TimeOfStop.AddMinutes(30) && dateE > stop.TimeOfStop.AddMinutes(-30) && stop.Platform == route.PlatformArrival)
                        timeTableDisplay.ErrorDateArrivalAlreadyExist = true;
                }

                var timeTables = await db.TimeTable.ToListAsync();
                foreach (TimeTable timeT in timeTables)
                {
                    var routeCheck = routes.Where(r => r.ID_Route == timeT.ID_Route).FirstOrDefault();

                    if (routeCheck.ID_City_Departure == route.ID_City_Departure && dateS < timeT.DateTimeDeparted.AddMinutes(30) && dateS > timeT.DateTimeDeparted.AddMinutes(-30) && routeCheck.PlatformDeparture == route.PlatformDeparture)
                        timeTableDisplay.ErrorDateDepartureAlreadyExist = true;

                    if (routeCheck.ID_City_Arrival == route.ID_City_Departure && dateS < timeT.DateTimeArrived.AddMinutes(30) && dateS > timeT.DateTimeArrived.AddMinutes(-30) && routeCheck.PlatformArrival == route.PlatformDeparture)
                        timeTableDisplay.ErrorDateDepartureAlreadyExist = true;

                    if (routeCheck.ID_City_Departure == route.ID_City_Arrival && dateE < timeT.DateTimeDeparted.AddMinutes(30) && dateE > timeT.DateTimeDeparted.AddMinutes(-30) && routeCheck.PlatformDeparture == route.PlatformArrival)
                        timeTableDisplay.ErrorDateArrivalAlreadyExist = true;

                    if (routeCheck.ID_City_Arrival == route.ID_City_Arrival && dateE < timeT.DateTimeArrived.AddMinutes(30) && dateE > timeT.DateTimeArrived.AddMinutes(-30) && routeCheck.PlatformArrival == route.PlatformArrival)
                        timeTableDisplay.ErrorDateArrivalAlreadyExist = true;
                }

                if (timeTableDisplay.ErrorDateArrivalAlreadyExist || timeTableDisplay.ErrorDateDepartureAlreadyExist || timeTableDisplay.ErrorDate)
                    return View(timeTableDisplay);

                db.TimeTable.Add(timeTableDisplay.timeTable);
                await db.SaveChangesAsync();
                return Redirect("TimeTable");
            }
            return View(timeTableDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> AddStopTimeTable(TimeTableDisplay timeTableDisplay)
        {
            timeTableDisplay.cities = await db.Cities.ToListAsync();
            timeTableDisplay.trains = await db.Train.ToListAsync();
            timeTableDisplay.routes = await db.Route.ToListAsync();


            return View("AddTimeTable", timeTableDisplay);
        }
        
        public async Task<IActionResult> AddUser()
        {
            userDisplay userDisplay = new userDisplay();
            userDisplay.users = await db.User.ToListAsync();
            userDisplay.roles = await db.Role.ToListAsync();
            return View(userDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(userDisplay userDisplay)
        {
            userDisplay.roles = await db.Role.ToListAsync();
            if (ModelState.IsValid)
            {
                db.User.Add(userDisplay.user);
                await db.SaveChangesAsync();
                return RedirectToAction("InfoUser");
            }
            return View(userDisplay);
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = await db.User.FirstAsync(s => s.ID_User == id);
            if (userToDelete != null)
            {
                db.User.Remove(userToDelete);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("InfoUser");
        }

        public async Task<IActionResult> InfoUser()
        {
            userDisplay userDisplay = new userDisplay();
            userDisplay.roles = await db.Role.ToListAsync();
            userDisplay.users = await db.User.ToListAsync();
            return View(userDisplay);
        }

        public async Task<IActionResult> AddTrain()
        {
            trainDisplay trainDisplay = new trainDisplay();
            trainDisplay.typeOfTrains = await db.TypeOfTrain.ToListAsync();
            return View(trainDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrain(trainDisplay trainDisplay)
        {
            trainDisplay.typeOfTrains = await db.TypeOfTrain.ToListAsync();
            if (ModelState.IsValid)
            {
                db.Train.Add(trainDisplay.train);
                await db.SaveChangesAsync();
                return RedirectToAction("InfoTrain");
            }
            return View(trainDisplay);
        }

        public async Task<IActionResult> DeleteTrain(int id)
        {
            var trainToDelete = await db.Train.FirstAsync(s => s.ID_Train == id);
            if (trainToDelete != null)
            {
                db.Train.Remove(trainToDelete);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("InfoTrain");
        }

        public async Task<IActionResult> InfoTrain()
        {
            var trainDisplay = new trainDisplay();
            trainDisplay.typeOfTrains = await db.TypeOfTrain.ToListAsync();
            trainDisplay.trains = await db.Train.ToListAsync();
            return View(trainDisplay);
        }

        public IActionResult AddTypeOfTrain()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTypeOfTrain(TypeOfTrain typeOfTrain)
        {
            if (ModelState.IsValid)
            {
                db.TypeOfTrain.Add(typeOfTrain);
                await db.SaveChangesAsync();
                return Redirect("InfoTypeOfTrain"); 
            }
            return View(typeOfTrain);
        }

        public async Task<IActionResult> DeleteTypeOfTrain(int id)
        {
            var typeoftrainToDelete = await db.TypeOfTrain.FirstAsync(s => s.ID_TypeOfTrain == id);
            if (typeoftrainToDelete != null)
            {
                db.TypeOfTrain.Remove(typeoftrainToDelete);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("InfoTypeOfTrain");
        }

        public async Task<IActionResult> InfoTypeOfTrain()
        {
            return View(await db.TypeOfTrain.ToListAsync());
        }


        public IActionResult FullCities()
        {
            return View();
        }

        public async Task<IActionResult> StaffOfTeam()
        {
            BrigadeDisplay brigadeDisplay = new BrigadeDisplay();
            brigadeDisplay.staffs = await db.staff.ToListAsync();
            brigadeDisplay.staffOfTeams = await db.StaffOfTeam.ToListAsync();
            brigadeDisplay.trains = await db.Train.ToListAsync();
            brigadeDisplay.doljnosts = await db.Doljnost.ToListAsync();
            return View(brigadeDisplay);
        }

        public async Task<IActionResult> DeleteStaffOfTeam(int id)
        {
            var staffOfTeam = await db.StaffOfTeam.FirstAsync(s => s.ID_SOT == id);
            if (staffOfTeam != null)
            {
                db.StaffOfTeam.Remove(staffOfTeam);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("StaffOfTeam");
        }

        public async Task<IActionResult> AddStaffOfTeam()
        {
            BrigadeDisplay brigadeDisplay = new BrigadeDisplay();
            brigadeDisplay.staffs = await db.staff.ToListAsync();
            brigadeDisplay.staffOfTeams = await db.StaffOfTeam.ToListAsync();
            brigadeDisplay.trains = await db.Train.ToListAsync();
            brigadeDisplay.doljnosts = await db.Doljnost.ToListAsync();
            return View(brigadeDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaffOfTeam(BrigadeDisplay brigadeDisplay)
        {
            brigadeDisplay.staffs = await db.staff.ToListAsync();
            brigadeDisplay.staffOfTeams = await db.StaffOfTeam.ToListAsync();
            brigadeDisplay.trains = await db.Train.ToListAsync(); 
            brigadeDisplay.doljnosts = await db.Doljnost.ToListAsync();
            if (ModelState.IsValid)
            {
                db.StaffOfTeam.Add(brigadeDisplay.StaffOfTeam);
                await db.SaveChangesAsync();
                return RedirectToAction("StaffOfTeam");
            }
            return View(brigadeDisplay);
        }

        public async Task<IActionResult> TimeTable()
        {
            TimeTableDisplay timeTableDisplay = new TimeTableDisplay();
            timeTableDisplay.cities = await db.Cities.ToListAsync();
            timeTableDisplay.trains = await db.Train.ToListAsync();
            timeTableDisplay.routes = await db.Route.ToListAsync();
            timeTableDisplay.timeTables = await db.TimeTable.ToListAsync();
            return View(timeTableDisplay);
        }

        public async Task<IActionResult> DeleteTimeTable(int id)
        {
            var timeTableToDelete = await db.TimeTable.FirstAsync(s => s.ID_TimeTable == id);
            var stops = await db.Stops.ToListAsync();
            if (timeTableToDelete != null)
            {
                foreach (Stops stop in stops)
                {
                    if (stop.ID_TimeTable == timeTableToDelete.ID_TimeTable) db.Stops.Remove(stop);
                }
                db.TimeTable.Remove(timeTableToDelete);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("TimeTable");
        }

        public async Task<IActionResult> AddStopTimeTable(int id)
        {
            TimeTableDisplay timeTableDisplay = new TimeTableDisplay();
            timeTableDisplay.cities = await db.Cities.ToListAsync();
            timeTableDisplay.trains = await db.Train.ToListAsync();
            timeTableDisplay.routes = await db.Route.ToListAsync();
            timeTableDisplay.timeTables = await db.TimeTable.ToListAsync();
            timeTableDisplay.stops = await db.Stops.ToListAsync();
            timeTableDisplay.timeTable = timeTableDisplay.timeTables.FirstOrDefault(t => t.ID_TimeTable == id);
            return View(timeTableDisplay);
        }

        [HttpPost("AddStopTimeTable/{id}")]
        public async Task<IActionResult> AddStopTimeTable(TimeTableDisplay timeTableDisplay, int id)
        {
            timeTableDisplay.cities = await db.Cities.ToListAsync();
            timeTableDisplay.trains = await db.Train.ToListAsync();
            timeTableDisplay.routes = await db.Route.ToListAsync();
            timeTableDisplay.timeTables = await db.TimeTable.ToListAsync();
            timeTableDisplay.stops = await db.Stops.ToListAsync();
            timeTableDisplay.timeTable = timeTableDisplay.timeTables.FirstOrDefault(t => t.ID_TimeTable == id);
            if (ModelState.IsValid)
            {
                var selectedRoute = timeTableDisplay.routes.Where(r => r.ID_Route == timeTableDisplay.timeTable.ID_Route).FirstOrDefault();
                if (selectedRoute.ID_City_Arrival == timeTableDisplay.stopToAdd.ID_City || selectedRoute.ID_City_Departure == timeTableDisplay.stopToAdd.ID_City)
                    timeTableDisplay.ErrorCityAlreadyExist = true;

                var dateStart = timeTableDisplay.timeTable.DateTimeDeparted;
                var dateEnd = timeTableDisplay.timeTable.DateTimeArrived;
                var dateStopCheck = timeTableDisplay.stopToAdd.TimeOfStop;
                if (dateStopCheck <= dateStart || dateStopCheck >= dateEnd)
                    timeTableDisplay.ErrorDateStop = true;

                var stops = timeTableDisplay.stops;
                foreach (Stops stop in stops)
                {
                    if (stop.ID_City == timeTableDisplay.stopToAdd.ID_City && dateStopCheck < stop.TimeOfStop.AddMinutes(30) && dateStopCheck > stop.TimeOfStop.AddMinutes(-30) && stop.Platform == timeTableDisplay.stopToAdd.Platform)
                        timeTableDisplay.ErrorDateAlreadyExist = true;

                    if (stop.ID_TimeTable == timeTableDisplay.timeTable.ID_TimeTable && stop.TimeOfStop == timeTableDisplay.stopToAdd.TimeOfStop)
                        timeTableDisplay.ErrorDateStopAlreadyExist = true;

                    if (stop.ID_City == timeTableDisplay.stopToAdd.ID_City && stop.ID_TimeTable == timeTableDisplay.timeTable.ID_TimeTable)
                        timeTableDisplay.ErrorCityAlreadyExist = true;
                }

                var timeTables = timeTableDisplay.timeTables;
                foreach (TimeTable timeTable in timeTables)
                {
                    var route = timeTableDisplay.routes.Where(r => r.ID_Route == timeTable.ID_Route).FirstOrDefault();
                    if (route.ID_City_Departure == timeTableDisplay.stopToAdd.ID_City && dateStopCheck < timeTable.DateTimeDeparted.AddMinutes(30) && dateStopCheck > timeTable.DateTimeDeparted.AddMinutes(-30) && route.PlatformDeparture == timeTableDisplay.stopToAdd.Platform)
                        timeTableDisplay.ErrorDateAlreadyExist = true;

                    if (route.ID_City_Arrival == timeTableDisplay.stopToAdd.ID_City && dateStopCheck < timeTable.DateTimeArrived.AddMinutes(30) && dateStopCheck > timeTable.DateTimeArrived.AddMinutes(-30) && route.PlatformArrival == timeTableDisplay.stopToAdd.Platform)
                        timeTableDisplay.ErrorDateAlreadyExist = true;
                }

                if (timeTableDisplay.stopToAdd.Platform > timeTableDisplay.cities.FirstOrDefault(c => c.ID_City == timeTableDisplay.stopToAdd.ID_City).PlatformCount)
                    timeTableDisplay.ErrorPlatformCity = true;

                if (timeTableDisplay.ErrorCityAlreadyExist || timeTableDisplay.ErrorDateAlreadyExist || timeTableDisplay.ErrorDateStop || timeTableDisplay.ErrorPlatformCity || timeTableDisplay.ErrorDateStopAlreadyExist)
                    return View(timeTableDisplay);

                timeTableDisplay.stopToAdd.ID_TimeTable = timeTableDisplay.timeTable.ID_TimeTable;
                db.Stops.Add(timeTableDisplay.stopToAdd);
                await db.SaveChangesAsync();

                timeTableDisplay.stops = await db.Stops.ToListAsync();
                return View(timeTableDisplay);
            }
            return View(timeTableDisplay);
        }
    }
}
