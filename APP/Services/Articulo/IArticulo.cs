using Api.Dtos.Articulo;

namespace APP.Services.Articulo
{
    public interface IArticulo
    {
        public Task<List<ArticuloDto>> ListadoArticulo(string strToken);
        public Task<List<DetailArticuloDto>> GetArticulo_Posicion_NumeroParte(string strToken, string strCodSucursal, string strCodArticulo);
        public Task<List<ArticuloDto>> SearchArticulo(string strToken,string strBusqueda);
    }
}
