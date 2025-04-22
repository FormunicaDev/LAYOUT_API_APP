using Api.Dtos.Bodega;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.User
{
    public class UserDetailDto
    {
        public string strUsuario { set; get; }
        public List<UserActionsDto> Acciones { set; get; }
        public UserRolDto Rol { set; get; }
        public List<BodegaDto> Bodegas { set; get; }
        public string strToken { set; get; }
    }
}
