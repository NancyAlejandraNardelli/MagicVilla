using MagicVilla_api.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_api.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                Id = 1,
                Nombre = "Villa Real",
                Detalle = "Detalle de la Villa....",
                Tarifa = 200,
                Ocupantes = 5,
                MetrosCuadrados = 50,
                ImagenUrl = "",
                Amenidad = "",
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            },

            new Villa()
            {
                Id = 2,
                Nombre = "Villa Real",
                Detalle = "Premium vista a la Piscina",
                Tarifa = 150,
                Ocupantes = 4,
                MetrosCuadrados = 40,
                ImagenUrl = "",
                Amenidad = "",
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            }
            );
        }
    }
}
