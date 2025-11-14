using Microsoft.AspNetCore.Mvc;
using SkyLine.Models;
using System.Threading.Tasks;

namespace SkyLine.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class HomeController : Controller
    {
        private readonly IRepository<Flight> _flight;


        public HomeController(IRepository<Flight> flight)
        {
            _flight = flight;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var flights = (await _flight.GetAsync(includes: [e => e.AirLine!, e => e.LeavingAirport!, e => e.ArriveAirport!])).AsQueryable();


            double totalPages = Math.Ceiling(flights.Count() / 6.0);
            int currentPage = page;

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = currentPage;

            flights = flights.Skip((page - 1) * 6).Take(6);

            ViewBag.Flights = flights.ToList();
            return View();
        }
    }
}
