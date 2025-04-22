using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos.Common
{
    public class ResponseWrapper<T>
    {
        [JsonProperty("result")]
        public T Resultado { get; set; }
    }
}
