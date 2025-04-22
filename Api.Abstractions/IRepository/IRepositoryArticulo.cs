using Api.Dtos.Articulo;
using Api.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Abstractions.IRepository
{
    public interface IRepositoryArticulo
    {
        public Task<ResultDto<List<ArticuloDto>>> GetArticulo(string strUsuario);
        public Task<ResultDto<List<DetailArticuloDto>>> GetArticulo_Posicion_NumeroParte(string strCodSucursal, string strArticulo);
        public Task<ResultDto<ArticuloDto>> SearchArticulo(string strBusqueda);
    }
}
