using System.Web;
using System.Web.Mvc;

namespace MyJSGames
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Locks everything that doesn't explicitly allow anonymous access.
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
