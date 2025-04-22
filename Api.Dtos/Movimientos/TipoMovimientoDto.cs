using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Movimientos
{
    public class TipoMovimientoDto
    {
        public int intIdTipoMovimiento {  get; set; }
        public string ?strTipoMovimiento { get; set; }=string.Empty;
    }
}
