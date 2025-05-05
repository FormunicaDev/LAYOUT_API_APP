using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Movimientos
{
    public class tempMovimientoDetalleRequestDto
    {
        public int intIdRelacionArticuloPosicion { get; set; } 
        public int intIdLinea {  get; set; }
        public decimal decCantidad { get; set; }  
        public string? strUsuario { get; set; }  
        public Guid strIdUsuario { get; set; }  
    }
}
