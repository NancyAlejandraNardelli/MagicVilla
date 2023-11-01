using MagicVilla_api.Datos;
using MagicVilla_api.Modelos;
using MagicVilla_api.Repositorio.IRepositorio;

namespace MagicVilla_api.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public VillaRepositorio(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion=DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
