using System.Threading.Tasks;
//using Jdenticon;
using Microsoft.AspNetCore.Mvc;

namespace masifAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class BildeController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                /* var image = "006623880005.jpg";
                File image;
                await ms.ReadAsync(image);
                await icon.SaveAsPngAsync(ms); */

                
                ms.Position = 0;
                return File(ms.ToArray(), "image/png");
            }
        }
    }
