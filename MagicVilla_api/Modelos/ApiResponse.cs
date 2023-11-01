using System.Net;

namespace MagicVilla_api.Modelos
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsExitoso { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public Object Resultado { get; set; }

    }

}
