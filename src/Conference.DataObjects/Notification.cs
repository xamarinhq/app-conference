using System;

namespace Conference.DataObjects
{
    public class Notification : BaseDataObject
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}

