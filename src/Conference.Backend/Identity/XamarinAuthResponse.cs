using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Backend.Identity
{
    public class XamarinAuthResponse
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public XamarinUser User { get; set; }

        public string Token { get; set; }
    }
}
