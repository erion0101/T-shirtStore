using DTOs.DTOs;
using DTOs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductServices _services;

        public OrderProductController(IOrderProductServices services)
        {
            _services = services;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add(OrderProductDTO dto, CancellationToken token)
        {
            try
            {
                //await _services.AddOrder(dto,token);
                return Ok(_services.AddOrder(dto, token));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       "Error adding data in database");
            }
        }
    }
}
