using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.User
{
    public class Jwt
    {
        public string key { set; get; }
        public string Issuer { set; get; }
        public string Audience { set; get; }
        public string Subject { set; get; }
    }
}
