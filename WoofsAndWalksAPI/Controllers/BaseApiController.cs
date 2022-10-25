using Microsoft.AspNetCore.Mvc;
using WoofsAndWalksAPI.Helpers;

namespace WoofsAndWalksAPI.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/{controller}")]
    public class BaseApiController : ControllerBase
    {

    }

}
