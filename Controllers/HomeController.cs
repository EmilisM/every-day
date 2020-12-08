using Microsoft.AspNetCore.Mvc;

namespace React.Sample.Webpack.CoreMvc.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
