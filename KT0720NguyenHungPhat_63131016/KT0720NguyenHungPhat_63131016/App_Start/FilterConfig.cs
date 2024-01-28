using System.Web;
using System.Web.Mvc;

namespace KT0720NguyenHungPhat_63131016
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
