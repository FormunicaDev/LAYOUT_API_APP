using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.User
{
    public class LoginDto
    {
        [Required(ErrorMessage ="CAMPO  REQUERIDO")]
        public string strUsuario { get; set; }
        [Required(ErrorMessage = "CAMPO REQUERIDO")]
        public string strPassword { get; set; }

    }
}
