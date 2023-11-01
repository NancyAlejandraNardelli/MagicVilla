using MagicVilla_api.Modelos;

namespace MagicVilla_api.Repositorio.IRepositorio
{
    public interface INumeroVillaRepositorio: IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
    }
}
