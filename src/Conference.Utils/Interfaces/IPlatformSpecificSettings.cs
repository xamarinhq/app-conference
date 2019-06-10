using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Clients.Portable
{
    public interface IPlatformSpecificSettings
    {
        string UserIdentifier { get; set; }
    }
}
