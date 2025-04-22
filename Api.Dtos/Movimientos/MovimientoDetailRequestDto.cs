using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Movimientos
{
    public class MovimientoDetailRequestDto
    {
        public decimal decCantidad {  get; set; }
        public int intIdMovimientoArticulo {  get; set; }
        public int intIdRelacionArticuloPosicion {  get; set; }
        public string? strUsuario { get; set; }=string.Empty;
    }
}
