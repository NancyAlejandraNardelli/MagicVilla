using MagicVilla_api.Modelos;

namespace MagicVilla_api.Repositorio.IRepositorio
{
    public interface IVillaRepositorio: IRepositorio<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
    }
}
