using System.ComponentModel.DataAnnotations;

namespace hoteles.Models;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public int? Stars { get; set; }
    public double? AveragePrice { get; set; }
    public bool Active { get; set; }
    public DateOnly? OpeningDate { get; set; }
    public HotelType? HotelType { get; set; }

    // UN Hotel puede tener MUCHAS Habitaciones (relación Uno a Muchos)
    public List<Room> Rooms { get; set; } = new(); // propiedad de navegación
}