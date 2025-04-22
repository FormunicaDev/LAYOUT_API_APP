using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.RelacionArticulo
{
    public class RelacionArticuloDto
    {
        public int intIdRelacionArticuloPosicion { set; get; }
        public string? strCodArticulo {  set; get; }
        public string? strDescripcionArticulo { set; get; }
        public double decPesoKg { set; get; }
        public decimal decUnidadesFisica { get; set; }
        public int intIdPosicion {  get; set; }
        public string? strPosicion { get; set; }
        public int intIdPrioridad {  set; get; }
        public string? strPrioridad { set; get; }
        public int intIdUnidadMedida {  get; set; }
        public string? strUnidadMedida {  set; get; }
        public string? strCodSucursal {  set; get; }
        public string? strUsuario { set; get; } 
    }
}
