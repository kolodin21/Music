using Microsoft.AspNetCore.Mvc;

namespace Music.Extensions;

public static class ControllerExtensions
{
    public static string GetName(this Controller controller)
    {
        return controller.GetType().Name.Replace("Controller", "");
    }
}