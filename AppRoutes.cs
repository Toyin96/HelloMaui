namespace HelloMaui;

public static class AppRoutes
{
    public const string BaseRoute = $"//{nameof(ListPage)}/";
    public const string DetailsRoute = $"//{nameof(ListPage)}/{nameof(DetailsPage)}";


    /// <summary>
    /// Constructs a route string dynamically based on the type parameter.
    /// </summary>
    /// <typeparam name="T">Type of the page which is derived from BaseContentPage.</typeparam>
    /// <returns>A formatted route string for navigation.</returns>
    public static string FetchRoute<T>() where T : ContentPage
    {
        string url;

        url = typeof(T) == typeof(ListPage) ? AppRoutes.BaseRoute : $"{BaseRoute}{typeof(T).Name}";

        return url;
    }
}