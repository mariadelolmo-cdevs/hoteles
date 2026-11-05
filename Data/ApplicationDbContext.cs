using hoteles.Models;
using Microsoft.EntityFrameworkCore;

namespace hoteles.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // tabla Hoteles
    public DbSet<Hotel> Hotels => Set<Hotel>();
    // tabla Habitaciones
    public DbSet<Room> Rooms => Set<Room>();
}