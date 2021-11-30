using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CulturaController : Controller
    {
        public IActionResult SetearCultura(string cultura, string redireccionURL)
        {
            if (cultura != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(cultura)));
            }

            return LocalRedirect(redireccionURL);
        }
    }
}
