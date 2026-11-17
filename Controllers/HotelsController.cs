using hoteles.Data;
using hoteles.Models;
using hoteles.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hoteles.Controllers;

public class HotelsController : Controller
{
    private readonly ApplicationDbContext context;

    public HotelsController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // listar hoteles con filtros opcionales
    public IActionResult Index(string? search, HotelType? hotelType) // GET /Hotels?search=lav&hotelType=Beach
    {
        var query = context.Hotels.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(h => h.Name.ToLower().Contains(search.ToLower()));
        }

        if (hotelType != null)
        {
            query = query.Where(h => h.HotelType == hotelType);
        }

        ViewBag.Search = search;
        ViewBag.HotelType = hotelType;

        var hoteles = query.OrderBy(h => h.Name).ToList();
        return View(hoteles);
    }

    // detalle de un hotel
    public IActionResult Details(int id)
    {
        var hotel = context.Hotels
            .Include(h => h.Rooms) // carga también las habitaciones del hotel
            .FirstOrDefault(h => h.Id == id);
        
        // si no existe el hotel
        if (hotel == null)
        {
            return NotFound();
        }

        var viewModel = new HotelDetailsViewModel
        {
            Hotel = hotel,
            TotalRooms = hotel.Rooms.Count,
            AvailableRooms = hotel.Rooms.Count(r => r.Available),
            MinPrice = hotel.Rooms.Count > 0 ? hotel.Rooms.Min(r => r.Price) : null
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var hotel = new Hotel { Active = true };

        return View("Form", hotel);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var hotel = context.Hotels.Find(id);

        if (hotel == null)
        {
            return NotFound();
        }

        return View("Form", hotel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(Hotel hotel)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", hotel);
        }

        if (hotel.Id == 0)
        {
            context.Hotels.Add(hotel);
        }
        else
        {
            context.Hotels.Update(hotel);
        }

        context.SaveChanges();

        TempData["Message"] = "Hotel guardado correctamente.";
        return RedirectToAction("Details", new { id = hotel.Id });
    }

    // desactivar un hotel (borrado lógico)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Deactivate(int id)
    {
        var hotel = context.Hotels.Find(id);
        
        if (hotel == null)
        {
            return NotFound();
        }

        hotel.Active = false;
        context.SaveChanges();

        TempData["Message"] = "Hotel desactivado.";
        return RedirectToAction("Details", new {id});
    }

    // borrar un hotel (borrado real)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var hotel = context.Hotels.Find(id);
        if (hotel == null)
        {
            return NotFound();
        }

        context.Hotels.Remove(hotel);
        context.SaveChanges();

        TempData["Message"] = "Hotel borrado.";
        return RedirectToAction("Index");
    }
}
