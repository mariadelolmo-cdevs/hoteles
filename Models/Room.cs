namespace hoteles.Models;

public class Room
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public double Price { get; set; }
    public bool Available { get; set; } // disponible

    // UNA Habitación pertenece a UN Hotel
    // esto será una columna en la tabla Rooms de base de datos
    public int HotelId { get; set; } // clave foránea (ES una COLUMNA)

    // esto no aparecerá en la tabla de BBDD
    public Hotel? Hotel { get; set; } // propiedad de navegación (NO es una COLUMNA)
}
