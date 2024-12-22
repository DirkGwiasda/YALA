using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yala.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class YalaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok(new { Message = "access granted!" });
        }

        /// <summary>
        /// http://localhost:5041/api/yala/public
        /// </summary>
        /// <returns></returns>
        //[HttpGet("public")]
        //[AllowAnonymous]
        //public IActionResult PublicEndpoint()
        //{
        //    return Ok(new { Message = "anonymous allowed" });
        //}
    }
}