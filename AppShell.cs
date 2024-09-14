namespace HelloMaui;

public class AppShell : Shell
{
    public AppShell(ListPage listPage)
    {
        Items.Add(listPage);

        RegisterApplicationRoutes();
    }

    private void RegisterApplicationRoutes()
    {
        Routing.RegisterRoute(AppRoutes.BaseRoute, typeof(ListPage));
        Routing.RegisterRoute(AppRoutes.DetailsRoute, typeof(DetailsPage));
    }
}