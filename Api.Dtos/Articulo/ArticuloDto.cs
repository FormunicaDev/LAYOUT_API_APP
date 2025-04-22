using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Articulo
{
    public class ArticuloDto
    {
        public string strCodArticulo { get; set; }
        public string strArticulo { get; set; }
        public string DescripcionCompleta=>strCodArticulo+" - "+strArticulo;
    }
}
