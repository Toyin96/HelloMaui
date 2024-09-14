namespace HelloMaui;

public class AppStyleHelperClass
{
    public static T GetStyle<T>(string styleName)
    {
        if (Application.Current!.Resources.TryGetValue(styleName, out var resource))
        {
            return (T)resource;
        };
        
        throw new KeyNotFoundException($"Could not find style {styleName}");
    }
}