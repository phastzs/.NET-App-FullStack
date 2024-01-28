using System.Web;
using System.Web.Mvc;

namespace ThiGK63CNTT2_63131016
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
