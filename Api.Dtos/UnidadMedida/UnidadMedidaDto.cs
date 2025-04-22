using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.UnidadMedida
{
    public class UnidadMedidaDto
    {
        public int intIdUnidadMedida {  get; set; }
        public string? strUnidadMedida { get; set; }
        public decimal? decCantidadUnidad {  get; set; }
        public string? strCodSucursal {  get; set; }
        public Int32 intActivo {  get; set; } 
        public DateTime datFechaCrea {  get; set; }
        public DateTime datFechaUpd {  get; set; }
        public string ?strUsuario {  get; set; }   
    }
}
