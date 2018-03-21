using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Conference.DataObjects;

namespace Conference.Backend.Models {
    public class Photo : BaseDataObject 
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}