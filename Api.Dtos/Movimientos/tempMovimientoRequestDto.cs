using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Movimientos
{
    public class tempMovimientoRequestDto
    {
        public string strDescripcion { get; set; }
        public string strNumeroDocumento { get; set; }
        public int intIdTipoMovimiento {  get; set; }
        public string strUsuario { get; set; }
        public Guid strIdUsuario { get; set; }  

    }
 
}
