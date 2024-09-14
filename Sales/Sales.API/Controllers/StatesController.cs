using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Sales.API.Data;
using Sales.Shared.Entidades;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/states")]
    public class StatesController(AppDbContext _context) : ControllerBase
    {
        [HttpGet("list/{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
	        var consulta = await _context.States
		        .Include(z => z.Cities!)
		        .Where(z=> z.CountryId == id)
				.ToListAsync();
			return Ok(consulta);
        }

		[HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
	        var consulta = await _context.States.FirstOrDefaultAsync(z=> z.Id == id);
	        if (consulta == null) return NotFound();
	        return Ok(consulta);
        }

		[HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody]State state)
        {
            try
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
	            return BadRequest(ex.GetExceptionMessage());
            }
		}

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody]State state)
        {
	        try
	        {
				_context.Update(state);
				await _context.SaveChangesAsync();
				return Ok();
			}
	        catch (DbUpdateException ex)
	        {
		        return BadRequest(ex.GetExceptionMessage());
	        }
		}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
	        var consulta = await _context.States.FirstOrDefaultAsync(z => z.Id == id);
	        if (consulta == null) return NotFound();

			_context.Remove(consulta);
	        await _context.SaveChangesAsync();
	        return Ok();
        }
	}
}
