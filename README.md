# Conference Mobile App

![](art/apps.png)

## Download from App Store
* [iOS: App Store](https://itunes.apple.com/us/app/xamarin-conference/id618319027) 
* [Android: Google Play](https://play.google.com/store/apps/details?id=com.xamarin.Conference)
* [Windows 10: Marketplace](https://www.microsoft.com/en-us/store/apps/xamarin-conference/9nblggh0ff9k) (Mobile & Desktop)

The Conference app is full of awesome and includes everything that you would expect from a spectacular conference application, but features tons of deep integration with:

* Azure + Online/Offline Sync
* Barcode Scanning
* Calendar Integration
* Maps & Navigation
* Push Notifications
* Phone Dialer
* Wi-Fi configuration
* URL Navigation (Universal Links + Google App Indexing)
* A bunch of other great things

## Shared code details
This app is around 15,000 lines of code. The iOS version contains 93% shared code, the Android version contains 90% shared code, the UWP has 99% shared code, and our Azure backend contains 23% shared code with the clients!:
        
<table>
  <tr>
    <td>
      <img src="http://chart.googleapis.com/chart?chtt=iOS%20app&cht=p&chs=500x220&chl=iOS-specific%20(7%)|Shared%20(93%)&chd=t:7,93&chco=9378CD|44B8A8"/>
    </td>
    <td>
      <img src="http://chart.googleapis.com/chart?chtt=Android%20app&cht=p&chs=500x220&chl=Android-specific%20(10%)|Shared%20(90%)&chd=t:10,90&chco=91CA47|44B8A8"/>
    </td>
    </tr>
    <tr>
    <td>
      <img src="http://chart.googleapis.com/chart?chtt=UWP%20app&cht=p&chs=500x220&chl=UWP-specific%20(1%)|Shared%20(99%)&chd=t:1,99&chco=3A6EBB|44B8A8"/>
    </td>
    <td>
      <img src="http://chart.googleapis.com/chart?chtt=Server&cht=p&chs=500x220&chl=Server-specific%20(77%)|Shared%20(23%)&chd=t:77,23&chco=FFE23B|44B8A8"/>
    </td>
  </tr>
</table>

## Test Cloud Integration
With each push of code the Conference app was built with [Visual Studio Team Services](https://www.visualstudio.com/en-us/products/visual-studio-team-services-vs.aspx) and deployed to be tested on a plethora of apps in [App Center Test](http://appcenter.ms).

![](art/testcloud1.png)

![](art/testcloud2.png)


# Getting Started

## Mobile App
Open up src/Conference.sln, which contains the iOS, Android, and Windows project. Simply restore your NuGet packages and build the application. It will run out of the box and will work off of a sample backend that we have published. 

## Data Source
Out of the box the Conference Mobile app uses sample data provided by the Conference.DataStore.Mock. This is great for development, but you can also test against the test/development read-only Azure App Server Mobile Apps backend. Simply head to *Conference.Client.Portable/ViewModel/ViewModelBase.cs*.

Simply change:

```
public static void Init (bool mock = true)
```

to

```
public static void Init (bool mock = false)
```

# Additional setup

## Push Notifications
All of the code for Azure Notification Hubs has been integrated into the Conference application, you will just need to setup your Azure Notifcation Hub Keys and Google Keys. Please read through the [startup guide](https://azure.microsoft.com/en-us/documentation/articles/notification-hubs-overview/) and then fill in your keys in: **Conference.Utils/Helpers/Constants.cs**


## Google Maps API key (Android)
There is a “Debug” key that you can use out of the box, or you can configure your own. For Android, you'll need to obtain a Google Maps API key:
https://developer.xamarin.com/guides/android/platform_features/maps_and_location/maps/obtaining_a_google_maps_api_key/

Insert it in the Android project: `~/Properties/AndroidManifest.xml`:

    <application ...>
      ...
      <meta-data android:name="com.google.android.geo.API_KEY" android:value="GOOGLE_MAPS_API_KEY" />
      ...
    </application>


## Bing Maps API Key (UWP)

In App.xaml.cs in the Conference.UWP update `Xamarin.FormsMaps.Init(string.Empty);` with your API key from https://www.bingmapsportal.com/

## Build your own Backend

This repo contains a full backend that you can deploy to your own Azure App Service Mobile App Backend.

# About
The Conference mobile apps were handcrafted by Xamarins spread out all over the world.

**Development:**
* [James Montemagno](http://github.com/jamesmontemagno)
* [Pierce Boggan](http://github.com/pierceboggan)

**Design:**
* [Antonio García Aprea](http://github.com/deskfolio)

**Testing:**
* [Ethan Dennis](https://github.com/erdennis13)

**Many thanks to:**
* [Fabio Cavalcante](https://github.com/fabiocav)
* [Matisse Hack](https://github.com/MatisseHack)
* [Sweetkriti Satpathy](https://github.com/Sweekriti91)
* [Andrew Branch](https://github.com/andrewbranch)
