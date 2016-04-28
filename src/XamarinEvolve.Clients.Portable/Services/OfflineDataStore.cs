using System;
using XamarinEvolve.Portable.Interfaces;
using System.Threading.Tasks;
using XamarinEvolve.Portable.Model;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinEvolve.Portable.Services;

//[assembly:Dependency(typeof(OfflineDataStore))]
namespace XamarinEvolve.Portable.Services
{
    public class OfflineDataStore : IDataStore
    {
        #region IDataStore implementation
        public Task InitializeStore()
        {
            throw new NotImplementedException();
        }
        public Individual GetSpeaker(string name)
        {
            throw new NotImplementedException();
        }
        public Task<Session> GetSessionAsync(string id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Session>> GetSessionsAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Session>> GetSpeakerSessionsAsync(string speakerName)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Individual>> GetSpeakersAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Individual>> GetSponsorsAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Individual>> GetExhibitorsAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

