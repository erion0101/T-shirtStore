using DTOs.DTOs;
using DTOs.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TshirtController : ControllerBase
    {
        private readonly ITshirtServices _services;
        public TshirtController(ITshirtServices services)
        {
            _services = services;
        }
        [HttpGet]
        [Route("GetTshirtbyId/{id}")]
        public async Task<IActionResult> GetTshirtbyId(int id, CancellationToken token)
        {
            try
            {
                var item = await _services.GetTshirtID(id, token);
                if(item == null)
                {
                    return NotFound($"Shoes with ID {id} not found");
                }
                return Ok(item);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error get data from database");
            }
        }
        //[HttpPost]
        //[Route("[action]/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //       await _services.Delete(id);
        //        return Ok("Tshirt deleting suceffuli");
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //          "Error deleting data from database");
        //    }
        //}
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTshirt(CancellationToken token)
        {
            try
            {
               return Ok( await _services.AllTshirt(token));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error geting data from database");
            }
        }
       

        [HttpPost]
        [Route("[action]/{tshirtDTO}")]
        public async Task<IActionResult> Add(TshirtDTO tshirtDTO, CancellationToken cancellationToken)
        {
            try
            {
              await _services.AddTshirt(tshirtDTO, cancellationToken);
                return Ok("Tshirt is Adding Succefulli");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding data from database");
            }
        }
        [HttpPost]
        [Route("[action]/{tshirtDTO}")]
        public async Task<IActionResult> Update(TshirtDTO tshirtDTO, CancellationToken cancellationToken)
        {
            try
            {
                await _services.Update(tshirtDTO, cancellationToken);
                return Ok("Tshirt is Updating Succefulli");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error update data from database");
            }
        }
        [HttpGet]
        [Route("[action]/{serchingTshirt}")]
        public async Task<IActionResult> Serche(string serchingTshirt)
        {
            try
            {
                return Ok(await _services.Serche(serchingTshirt));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error serching data from database");
            }
        }

    }
}
