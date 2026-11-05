using hoteles.Models;

namespace hoteles.ViewModels;

public class HotelDetailsViewModel
{
    public Hotel Hotel { get; set; } = new();
    public int TotalRooms { get; set; }
    public int AvailableRooms { get; set; }
    public double? MinPrice { get; set; }
}
