using DTOs.DTOs;
using DTOs.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsereController : ControllerBase
    {
        private readonly IUsereServices _usereServies;
        public UsereController(IUsereServices usereServies)
        {
            _usereServies = usereServies;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterUsere(UsereDTO usereDTO,CancellationToken token)
        {
            try
            {
                await _usereServies.RegisterUsere(usereDTO, token);
                return Ok("Usere createing sucefulli");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error create usere in database");
            }
        }
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> Regjistro(StudentiDto student, CancellationToken token)
        //{
        //    await _studentService.RegjistroStudentin(student, token);
        //    return Ok();
        //}
    }
}
