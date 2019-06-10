using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Clients.Portable.Interfaces
{
    public interface IPlatformSpecificSettings
    {
        string UserIdentifier { get; set; }
    }
}
