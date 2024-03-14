using DTOs.DTOs;
using DTOs.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemServices _services;
        public CartItemController(ICartItemServices services)
        {
            _services = services;
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _services.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Gabim në fshirjen e të dhënave nga baza e të dhënave");
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            try
            {
                 return Ok(await _services.AllTshirt(token));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
             "Error get data in database");
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add(CartItemTshirtDTO dto , CancellationToken token)
        {
            try
            {
                await _services.AddTshirt(dto, token);
                return Ok("Data is adding sucessfulli");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error adding data in database");
            }
        }
      
    }
}
