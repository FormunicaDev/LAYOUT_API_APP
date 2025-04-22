using Api.Abstractions.IRepository;
using Api.Abstractions.IServices;
using Api.Dtos.Common;
using Api.Dtos.RelacionArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.RelacionArticulo
{
    public class ServicesRelacionArticulo:IServicesRelacionArticulo
    {
        private readonly IRepositoryRelacionArticulo _RepositoryRelacionArticulo;
        public ServicesRelacionArticulo(IRepositoryRelacionArticulo irepositoryRelacionArticulo)
        {
            _RepositoryRelacionArticulo = irepositoryRelacionArticulo;
        }

        public async Task<ResultDto<int>> CreateOrUpdate(RelacionArticuloDto relacionArticuloDto)
        {
            return await _RepositoryRelacionArticulo.CreateOrUpdate(relacionArticuloDto);
        }

        public async Task<ResultDto<object>> GetAll(int intIdRelacionArticuloPosicion, string strCodSucursal)
        {
            return await _RepositoryRelacionArticulo.GetAll(intIdRelacionArticuloPosicion,strCodSucursal);
        }

        public async Task<ResultDto<object>> GetSearch(int intIdRelacionArticuloPosicion, string strCodSucursal)
        {
            return await _RepositoryRelacionArticulo.GetSearch(intIdRelacionArticuloPosicion, strCodSucursal);
        }

        public async Task<ResultDto<object>> GetSearchPosicionXArticulo(string strCodSucursal, string strCodArticulo)
        {
            return await _RepositoryRelacionArticulo.GetSearchPosicionXArticulo(strCodSucursal,strCodArticulo);
        }
    }
}
