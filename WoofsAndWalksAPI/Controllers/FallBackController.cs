using Microsoft.AspNetCore.Mvc;

namespace WoofsAndWalksAPI.Controllers;

public class FallBackController : Controller
{
    public ActionResult Index()
    {
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
    }
}