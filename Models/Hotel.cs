using System.ComponentModel.DataAnnotations;

namespace hoteles.Models;

public class Hotel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(60, ErrorMessage = "La ciudad no puede superar los {1} caracteres.")]
    public string? City { get; set; }

    [Range(1, 5, ErrorMessage = "Las estrellas deben estar entre {1} y {2}.")]
    public int? Stars { get; set; }

    [Range(0, 10000, ErrorMessage = "El precio medio debe estar entre {1} y {2}.")]
    public double? AveragePrice { get; set; }
    
    public bool Active { get; set; }
    public DateOnly? OpeningDate { get; set; }
    public HotelType? HotelType { get; set; }

    // UN Hotel puede tener MUCHAS Habitaciones (relación Uno a Muchos)
    public List<Room> Rooms { get; set; } = new(); // propiedad de navegación
}