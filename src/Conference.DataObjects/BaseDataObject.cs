using System;

#if BACKEND
using Microsoft.Azure.Mobile.Server;
#elif MOBILE
using MvvmHelpers;
#endif

namespace Conference.DataObjects
{

    public interface IBaseDataObject
    {
        string Id {get;set;}
    }
    #if BACKEND
    public class BaseDataObject : EntityData
    {
        public BaseDataObject ()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string RemoteId { get; set; }
    }
    #else
    public class BaseDataObject : ObservableObject, IBaseDataObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string RemoteId { get; set; }

        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
    }
    #endif
}

