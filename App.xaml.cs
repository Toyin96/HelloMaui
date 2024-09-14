using System.Diagnostics;

namespace HelloMaui;

public partial class App : Application
{
    public App(AppShell shell)
    {
        InitializeComponent();

        MainPage = shell;
    }

    protected override void OnStart()
    {
        base.OnStart();

        Trace.WriteLine("Application started======>");
    }

    protected override void OnSleep()
    {
        base.OnSleep();

        Trace.WriteLine("Application sleep======>");
    }

    protected override void OnResume()
    {
        base.OnResume();

        Trace.WriteLine("Application resumed======>");
    }
}