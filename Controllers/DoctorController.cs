using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        [HttpGet]
        [Route("GetAllDoctors")]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
