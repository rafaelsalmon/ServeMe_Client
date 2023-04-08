Multi-PLatform Mobile App with Xamarin.Forms.

It is the CLIENT mobile app on a home services marketplace. You can schedule appointments with service professionals, like plumbers, who are available and at your region, through a user experience similar to hailing a cab using the Uber app, provided that a service request can be more detailed than a ride request and the pricing is negotiated.

# UTILITY

After a little refactoring (see below), it can be used as is and it can also be customized and repurposed to fit any type of service marketplace. A good example is a domain-specific marketplace, like house cleanup.

I'm sharing this project's code initially as a showcase of my work, since it hasn't been commercially utilized. 

# DESIGN PATTERNS

You can see in this app the use of a few design patterns (some of which not fully implemented) such as:

* View Model;
* Repository;
* MVVM;
* Dependency Injection.
	There is a dependency injection on Repositorio\AcessoDados.cs, as an example of the pattern use.

It is intrinsic of a Xamarin.Forms' mobile app to make `asynchronous calls`, so this project also presents a good demonstration of this approach.

# RELEASE NOTES

It was developed as a freelance project by request of an entrepreneur who never brought the app to market. Stack:

* Built with Xamarin before Microsoft bought the company and turned this framework part of the .Net Framework;

* The version of the .Net Framework utilized was 4.5;

* Data is locally persisted in SQLite and sent to an api in Azure;

Other:

* The comments are in English. The app was built in Portuguese (with some of the layers in the architecture having been named in English), but I inserted English comments in the beginning of the files from the Model, View and ViewModel layers of the SirvaMe (Portable) project translating class, view and viewmodel names and/or briefly explaining what that item does.

* All multi-platform code is on the root project - SirvaMe (Portable).  Xamarin.Forms is responsible to transform the UI objects in Android or iOS objects when apropriate (when you compile the app for each O.S.);

* SirvaMe.Droid and SirvaMe.iOS differ in configurations and UI elements such as Renderers and Resources to cater for the two different platforms. Android Resources were designed for all main device dimensions.

# USAGE

Start by opening Docs\References and Dependecies.txt inside Sirvame (Portable) project. There you can find a documented list of components and versions utilized, links for more information about them, 
how to install them and configurations necessary for external dependencies such as Facebook app metadata.

## DEPRECATED COMPONENTS

When opening the solution in Visual Studio (tested with VS 2022 and wihtout perfoming any of the steps to install components described in the References and Dependencies.txt file mentioned above), even after installing some libraries that VS will ask you to, you will receive an error saying Xamarin Components is no longer supported. The link below shows this message and how to proceed to refactor the project references:
```
https://learn.microsoft.com/en-us/xamarin/cross-platform/troubleshooting/component-nuget?tabs=windows#Components_without_a_NuGet_migration_path
```

* This Visual Studio Project and Solution haven't been run since the end of the development project quite some time ago. So, after performing the above refactoring procedure (which I haven't), if you still find and/or fix further issues, please report;

* Replace the `[API URL]` value in the `App.xaml` file to the address of the cloud api containing the server-side services called by this mobile app:

```
<?xml version="1.0" encoding="utf-8" ?>
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="SirvaMe.App">
  <Application.Resources>
    <ResourceDictionary>
      <!--Default-->
      <x:String x:Key="ProjectName">Sirva-Me Cliente</x:String>
      <x:String x:Key="UrlApi">[API URL]</x:String>
```

* To test, you need to either simulate an Android device or connect one via USB port to the computer. You also need to allow developer access to your Android device in the device's configuration;

* The Android test can be done by connecting the USB cable and running the project from Visual Studio or by transferring the compiled app file (Release version) to the Android device. This is why it's easier to test on Android first;

* iOS packages usually need to go through the App Store so they can be installed. There is an official platform called TestFlight so you can test iOS apps though.
