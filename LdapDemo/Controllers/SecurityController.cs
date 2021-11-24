using LdapDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LdapDemo.Controllers
{
    [Route("api/")]
    public class SecurityController : Controller
    {
        private readonly IAuthenticationService authService;

        public SecurityController(IAuthenticationService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = authService.Login(userName, password);
            if (null != user)
            {
                // create your login token here
                return Ok("Logged Successfully");
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(authService.GetADUsers());
        }
    }
}