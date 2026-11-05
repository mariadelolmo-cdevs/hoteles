using System.ComponentModel.DataAnnotations;

namespace hoteles.Models;

public enum HotelType
{
    [Display(Name = "Ciudad")]
    City,
    [Display(Name = "Playa")]
    Beach,
    [Display(Name = "Montaña")]
    Mountain,
    [Display(Name = "Rural")]
    Rural
}
