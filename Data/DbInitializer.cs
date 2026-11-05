using Microsoft.EntityFrameworkCore;
using hoteles.Models;

namespace hoteles.Data;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        // aplica  migraciones pendientes al arrancar la app
        context.Database.Migrate();

        // crear hoteles demo
        if (!context.Hotels.Any()) // se ejecuta si la tabla Hotels está vacío
        {
            context.Hotels.AddRange(
                new Hotel { Name = "Hotel Lavandera Blanca", City = "Argentona", Stars = 4, AveragePrice = 95, Active = true, OpeningDate = new DateOnly(2010, 5, 20), HotelType = HotelType.Beach },
                new Hotel { Name = "Carbonero Común Palace", City = "Madrid", Stars = 5, AveragePrice = 180, Active = true, OpeningDate = new DateOnly(2001, 9, 14), HotelType = HotelType.City },
                new Hotel { Name = "Refugio de la Grajilla", City = "Jaca", Stars = 3, AveragePrice = 70, Active = true, OpeningDate = new DateOnly(1980, 2, 1), HotelType = HotelType.Mountain },
                new Hotel { Name = "Casa Rural La Oropéndola", City = "Jaén", Stars = 2, AveragePrice = 55, Active = false, OpeningDate = new DateOnly(1998, 7, 23), HotelType = HotelType.Rural }
            );
            context.SaveChanges();
        }

        // crear habitaciones demo
        if (!context.Rooms.Any())
        {
            var lavandera = context.Hotels.First(h => h.Name == "Hotel Lavandera Blanca");
            var carbonero = context.Hotels.First(h => h.Name == "Carbonero Común Palace");
            var refugio = context.Hotels.First(h => h.Name == "Refugio de la Grajilla");

            context.Rooms.AddRange(
                new Room { Number = "101", Capacity = 2, Price = 85, Available = true, Hotel = lavandera },
                new Room { Number = "102", Capacity = 2, Price = 85, Available = false, Hotel = lavandera },
                new Room { Number = "201", Capacity = 4, Price = 140, Available = true, Hotel = lavandera },
                new Room { Number = "301", Capacity = 1, Price = 120, Available = true, Hotel = carbonero },
                new Room { Number = "302", Capacity = 2, Price = 195, Available = true, Hotel = carbonero },
                new Room { Number = "1", Capacity = 2, Price = 60, Available = true, Hotel = refugio },
                new Room { Number = "2", Capacity = 3, Price = 80, Available = false, Hotel = refugio}
            );
            context.SaveChanges();
        }

        Console.WriteLine($"Hoteles en la base de datos: {context.Hotels.Count()}");
        Console.WriteLine($"Habitaciones en la base de datos: {context.Rooms.Count()}");
    }
}