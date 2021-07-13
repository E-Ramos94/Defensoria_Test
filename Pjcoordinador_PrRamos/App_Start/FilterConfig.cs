using System.Web;
using System.Web.Mvc;

namespace Pjcoordinador_PrRamos
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
