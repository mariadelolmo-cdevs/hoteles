using System.ComponentModel.DataAnnotations;

namespace hoteles.Models;

public class Persona
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "El correo no tiene formato válido.")]
    public string? Email { get; set; }

    [Phone]
    public string? Telefono { get; set; }

    [Url]
    public string? Web { get; set; }

    [RegularExpression("^[0-9A-Za-z]+$", ErrorMessage = "Sólo letras y números.")]
    public string Number { get; set; } = string.Empty;

    
    [Range(0, 10000, ErrorMessage = "El precio debe estar entre {1} y {2}")]
    public double? AveragePrice { get; set; }
}