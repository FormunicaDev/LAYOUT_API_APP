using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Posicion
{
    public class PosicionDto
    {
        public int intIdPosicion { get; set; }
        [Required (ErrorMessage ="CAMPO OBLIGATORIO(*)")]
        public string? strPosicion { get; set; }
        public string? strCodSucursal { get; set; }
        public string? strDescripcion {  get; set; }
        public DateTime? datFechaCrea { get; set; }
        public DateTime? datFechaUpd { get; set; }
        public string? strUsuario { get; set; }
        public string ?imgCodigoBarra { get; set; }
        public IFormFile? imgArchivo { get; set; }
        public int intIdTipoPosicion { set; get; }
        public decimal decCapacidad {  get; set; }
    }
}
