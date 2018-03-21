using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FormsToolkit;
using MvvmHelpers;
using Xamarin.Forms;
using Conference.DataObjects;
using System.Collections.Generic;

namespace Conference.Clients.Portable
{
    public class SponsorsViewModel : ViewModelBase
    {
        public SponsorsViewModel(INavigation navigation) : base(navigation)
        {

        }


        public ObservableRangeCollection<Sponsor> Sponsors { get; } = new ObservableRangeCollection<Sponsor>();
        public ObservableRangeCollection<Grouping<string, Sponsor>> SponsorsGrouped { get; } = new ObservableRangeCollection<Grouping<string, Sponsor>>();



        #region Properties
        Sponsor selectedSponsor;
        public Sponsor SelectedSponsor
        {
            get { return selectedSponsor; }
            set
            {
                selectedSponsor = value;
                OnPropertyChanged();
                if (selectedSponsor == null)
                    return;
                 
                MessagingService.Current.SendMessage(MessageKeys.NavigateToSponsor, selectedSponsor);

                SelectedSponsor = null;
            }
        }

     
        #endregion

        #region Sorting


        void SortSponsors(IEnumerable<Sponsor> sponsors)
        {
            var sponsorsRanked = from sponsor in sponsors
                                          orderby sponsor.Name, sponsor.Rank
                                          orderby sponsor.SponsorLevel.Rank
                                          select sponsor;

            Sponsors.ReplaceRange(sponsorsRanked);

            var groups = from sponsor in Sponsors
                group sponsor by sponsor.SponsorLevel.Name
                into sponsorGroup
                select new Grouping<string, Sponsor>(sponsorGroup.Key, sponsorGroup); 
            
            SponsorsGrouped.ReplaceRange(groups);
        }



        #endregion


        #region Commands

        ICommand  forceRefreshCommand;
        public ICommand ForceRefreshCommand =>
        forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync())); 

        async Task ExecuteForceRefreshCommandAsync()
        {
            await ExecuteLoadSponsorsAsync(true);
        }

        ICommand loadSponsorsCommand;
        public ICommand LoadSponsorsCommand =>
            loadSponsorsCommand ?? (loadSponsorsCommand = new Command(async (f) => await ExecuteLoadSponsorsAsync())); 

        async Task<bool> ExecuteLoadSponsorsAsync(bool force = false)
        {
            if(IsBusy)
                return false;

            try 
            {
                IsBusy = true;

                #if DEBUG
                await Task.Delay(1000);
                #endif
                var sponsors = await StoreManager.SponsorStore.GetItemsAsync(force);
               
                SortSponsors(sponsors);

            } 
            catch (Exception ex) 
            {
                Logger.Report(ex, "Method", "ExecuteLoadSponsorsAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }

            return true;
        }
           

        #endregion
    }
}

