using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Conference.Backend.Identity
{
    public sealed class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
