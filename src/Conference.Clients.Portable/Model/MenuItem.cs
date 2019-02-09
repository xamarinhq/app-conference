using System;
using System.Windows.Input;
using MvvmHelpers;

namespace Conference.Clients.Portable
{
    public class MenuItem : ObservableObject
    {
        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        string subtitle;
        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        public string Icon {get;set;}
        public string Parameter {get;set;}

        public AppPage Page { get; set; }
        public ICommand Command {get;set;}
    }
}

