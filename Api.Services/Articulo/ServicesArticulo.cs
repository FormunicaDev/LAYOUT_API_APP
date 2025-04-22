using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Articulo;
using Api.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Articulo
{
    public class ServicesArticulo : IServicesArticulo
    {
        private IRepositoryArticulo _RepositoryArticulo;
        public ServicesArticulo(IRepositoryArticulo repositoryArticulo) { 
            _RepositoryArticulo = repositoryArticulo;
        }
        public async Task<ResultDto<List<ArticuloDto>>> GetArticulo(string strUsuario)
        {
           return await _RepositoryArticulo.GetArticulo(strUsuario);
        }

        public async Task<ResultDto<List<DetailArticuloDto>>> GetArticulo_Posicion_NumeroParte(string strCodSucursal, string strArticulo)
        {
           return await _RepositoryArticulo.GetArticulo_Posicion_NumeroParte(strCodSucursal,strArticulo);
        }

        public async Task<ResultDto<ArticuloDto>> SearchArticulo(string strBusqueda)
        {
            return await _RepositoryArticulo.SearchArticulo(strBusqueda);
        }
    }
}
