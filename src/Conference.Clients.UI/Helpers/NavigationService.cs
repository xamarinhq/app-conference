using System.Threading.Tasks;
using Xamarin.Forms;

namespace Conference.Clients.UI
{
    /// <summary>
    /// Helper navigation service to use so we don't push multiple pages at the same time.
    /// </summary>
    public static class NavigationService
    {
        static bool navigating;
        /// <summary>
        /// PUsh a page async
        /// </summary>
        /// <returns>awaitable task.</returns>
        /// <param name="navigation">Navigation.</param>
        /// <param name="page">Page.</param>
        /// <param name="animate">If set to <c>true</c> animate.</param>
        public static async Task PushAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (navigating)
                return;

            navigating = true;
            await navigation.PushAsync(page, animate);
            navigating = false;
        }

        /// <summary>
        /// Push a page modal async
        /// </summary>
        /// <returns>awaitable task.</returns>
        /// <param name="navigation">Navigation.</param>
        /// <param name="page">Page.</param>
        /// <param name="animate">If set to <c>true</c> animate.</param>
        public static async Task PushModalAsync(INavigation navigation, Page page, bool animate = true)
        {
            if (navigating)
                return;

            navigating = true;
            await navigation.PushModalAsync(page, animate);
            navigating = false;
        }
    }
}

