using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyLine.Models;
using SkyLine.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkyLine.Areas.Customer.Controllers;

[Area(SD.CustomerArea)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Flight> _flight;
    private readonly IRepository<City> _City;
    private readonly IRepository<FlightSegment> _Segment;

    public HomeController(ILogger<HomeController> logger, IRepository<Flight> flight1, IRepository<FlightSegment> segment, IRepository<City> city)
    {
        _logger = logger;
        _flight = flight1;
        _Segment = segment;
        _City = city;
    }

    public async Task<IActionResult> Index()
    {
        var cities = await _City.GetAsync();

        return View(cities);
    }

    public async Task<IActionResult> Results(Filters flightFilter, int page = 1)
    {

        var flights = (await _flight.GetAsync(includes: [e => e.AirLine!, e => e.LeavingAirport!, e => e.ArriveAirport!])).AsQueryable();

        if (flightFilter.LeavingCity != null)
        {
            flights = flights.Where(e => e.LeavingAirport.cityId == flightFilter.LeavingCity);
        }

        if (flightFilter.ArrivingCity is not null)
        {
            flights = flights.Where(e => e.ArriveAirport.cityId == flightFilter.ArrivingCity);
        }

        if (flightFilter.dates is not null)
        {
            flights = flights.Where(e => e.Leaving_Time == flightFilter.dates);
        }



        double totalPages = Math.Ceiling(flights.Count() / 6.0); 
        int currentPage = page;

        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentPage = currentPage;

        flights = flights.Skip((page - 1) * 6).Take(6);



        
        return View(flights.ToList());
    }


    public async Task<IActionResult> Details([FromRoute]int id)
    {

       
        var flights22 = await _flight.GetOneAsync(includes: [e => e.AirLine!, e => e.LeavingAirport!.city, e => e.ArriveAirport!.city, e=>e.Fares],expression: e=>e.Flight_Id_PK == id);
        var Segment = await _Segment.GetAsync(expression: e => e.Flight_ID_Fk == id, includes: [e=>e.DepartureAirport!.city, e=>e.ArrivalAirport!.city]);

        InitialClass Initial = new InitialClass()
        {
            flights = flights22,
            flightSegment = Segment

        };
        return View(Initial);
    }

    public IActionResult Booking()
    {


        return View();
    }




}
