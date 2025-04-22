using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Articulo
{
    public class DetailArticuloDto
    {
        public string strCodArticulo { get; set; }
        public string strDescripcion { get; set; }
        public string strNumeroParte { get; set; }
        public int intIdPosicion { get; set; }
        public string strPosicion { get; set; }
        public int intIdPrioridad { get; set; }
        public string strPrioridad { get; set; }
        public decimal decCantidadDisponible { get; set; }
        public string strCodSucursal { get; set; }
        public string strUnidadMedida { get; set; }
    }
}
