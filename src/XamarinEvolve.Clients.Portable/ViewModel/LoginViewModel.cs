using System;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Plugin.Share;
using FormsToolkit;
using Plugin.Share.Abstractions;

namespace XamarinEvolve.Clients.Portable
{
    public class LoginViewModel : ViewModelBase
    {
        ISSOClient client;
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            client = DependencyService.Get<ISSOClient>();
        }

        string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        ICommand  loginCommand;
        public ICommand LoginCommand =>
            loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginAsync())); 

        async Task ExecuteLoginAsync()
        {
            if(IsBusy)
                return;

            if(string.IsNullOrWhiteSpace(email))
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title="Sign in Information",
                        Message="We do need your email address :-)",
                        Cancel ="OK"
                    });
                return;
            }

            if(string.IsNullOrWhiteSpace(password))
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title="Sign in Information",
                        Message="Password is empty!",
                        Cancel ="OK"
                    });
                return;
            }

            try 
            {
                IsBusy = true;
                Message = "Signing in...";
                #if DEBUG
                await Task.Delay(1000);
                #endif
                AccountResponse result = null;

                #if ENABLE_TEST_CLOUD
                if(email == "xtc@xamarin.com")
                {
                    result = new AccountResponse
                        {
                            Success = true,
                            User = new User 
                                {
                                    Email = "xtc@xamarin.com",
                                    FirstName = "XTC",
                                    LastName = "User"
                                }
                        };
                }
                #endif
                if(result == null)
                    result = await client.LoginAsync(email, password);
                
                if(result?.Success ?? false)
                {
                    Message = "Updating schedule...";
                    Settings.FirstName = result.User?.FirstName ?? string.Empty;
                    Settings.LastName = result.User?.LastName ?? string.Empty;
                    Settings.Email = email.ToLowerInvariant();
                    MessagingService.Current.SendMessage(MessageKeys.LoggedIn);
                    Logger.Track(EvolveLoggerKeys.LoginSuccess);
                    try
                    {
                        await StoreManager.SyncAllAsync(true);
                        Settings.Current.LastSync = DateTime.UtcNow;
                        Settings.Current.HasSyncedData = true;
                    }
                    catch(Exception ex)
                    {
                        //if sync doesn't work don't worry it is alright we can recover later
                        Logger.Report(ex);
                    }
                    await Finish();
                    Settings.FirstRun = false;
                }
                else
                {
                    Logger.Track(EvolveLoggerKeys.LoginFailure, "Reason", result.Error); 
                    MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                        {
                            Title="Unable to Sign in",
                            Message=result.Error,
                            Cancel ="OK"
                        });
                }
            } 
            catch (Exception ex) 
            {
                Logger.Track(EvolveLoggerKeys.LoginFailure, "Reason", ex?.Message ?? string.Empty);

                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title="Unable to Sign in",
                        Message="The email or password provided is incorrect.",
                        Cancel ="OK"
                    });
            }
            finally
            {
                Message = string.Empty;
                IsBusy = false;
            }
        }

        ICommand  signupCommand;
        public ICommand SignupCommand =>
            signupCommand ?? (signupCommand = new Command(async () => await ExecuteSignupAsync())); 

        async Task ExecuteSignupAsync()
        {
            Logger.Track(EvolveLoggerKeys.Signup);
            await CrossShare.Current.OpenBrowser("https://auth.xamarin.com/account/register", 
                new BrowserOptions
                {
                    ChromeShowTitle = true,
                    ChromeToolbarColor = new ShareColor
                        {
                            A=255,
                            R=118,
                            G=53,
                            B=235
                        },
                    UseSafariReaderMode = false,
                    UseSafariWebViewController = true
                });
        }

        ICommand  cancelCommand;
        public ICommand CancelCommand =>
            cancelCommand ?? (cancelCommand = new Command(async () => await ExecuteCancelAsync())); 

        async Task ExecuteCancelAsync()
        {
            Logger.Track(EvolveLoggerKeys.LoginCancel);
            if(Settings.FirstRun)
            {
                try 
                {
                    Message = "Updating schedule...";
                    IsBusy = true;
                    await StoreManager.SyncAllAsync(false);
                    Settings.Current.LastSync = DateTime.UtcNow;
                    Settings.Current.HasSyncedData = true;
                } 
                catch (Exception ex) 
                {
                    //if sync doesn't work don't worry it is alright we can recover later
                    Logger.Report(ex);
                }
                finally
                {
                    Message = string.Empty;
                    IsBusy = false;
                }
            }
            await Finish();
            Settings.FirstRun = false;
        }

        async Task Finish()
        {
            if(Device.RuntimePlatform == Device.iOS && Settings.FirstRun)
            {

                #if ENABLE_TEST_CLOUD
                MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.Question, new MessagingServiceQuestion
                    {
                        Title = "Push Notifications",
                        Positive = "Let's do it!",
                        Negative = "Maybe Later",
                        Question = "We can send you updates through Evolve via push notifications. Would you like to enable them now?",
                        OnCompleted = async (success) =>
                            {
                                if(success)
                                {
                                    var push = DependencyService.Get<IPushNotifications>();
                                    if(push != null)
                                        await push.RegisterForNotifications();
                                }

                                await Navigation.PopModalAsync();
                            }
                    });
#else

                    var push = DependencyService.Get<IPushNotifications>();
                    if(push != null)
                        await push.RegisterForNotifications();

                    await Navigation.PopModalAsync();
#endif
            }
            else
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}

