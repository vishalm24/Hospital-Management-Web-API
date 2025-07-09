using Hospital_Management.Model.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            return Ok(await _authService.Login(login));
        }
        [HttpPost]
        [Route("RegisterFirstAdmin")]
        public async Task<IActionResult> RegisterFirstAdmin([FromBody] RegisterRequest register)
        {
            return Ok(await _authService.RegisterFirstAdmin(register));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest register)
        {
            return Ok(await _authService.RegisterAdmin(register));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("RegisterReceptionist")]
        public async Task<IActionResult> RegisterReceptionist([FromBody] RegisterRequest register)
        {
            return Ok(await _authService.RegisterReceptionist(register));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("RegisterDoctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorRequest register)
        {
            return Ok(await _authService.RegisterDoctor(register));
        }
        [HttpPost]
        [Route("PasswordChange")]
        [Authorize]
        public async Task<IActionResult> PasswordChange(string password)
        {
            return Ok(await _authService.PasswordChange(password));
        }
        //[HttpPost]
        //[Route("RegisterFirstAdmin")]
        ////[Authorize("Admin")]
        ////[Authorize]
        //public async Task<IActionResult> RegisterFirstAdmin([FromBody]RegisterRequest register)
        //{
        //    return Ok(await _authService.RegisterAdmin(register));
        //}
    }
}
