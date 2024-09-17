using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API._Helpers;
using Sales.API.Data;
using Sales.Shared.DTO;
using Sales.Shared.Entidades;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController(AppDbContext _context) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
	        var consulta = _context.Cities.Where(z => z.StateId == pagination.Id).AsQueryable();

	        if (!string.IsNullOrWhiteSpace(pagination.Filter))
		        consulta = consulta.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
	        
			return Ok(await consulta
				.OrderBy(x => x.Name)
				.Paginate(pagination)
				.ToListAsync());
        }

        [HttpGet("totalpages")]
        public async Task<ActionResult<int>> GetPages([FromQuery] PaginationDTO pagination)
        {
	        var consulta = _context.Cities.Where(z => z.StateId == pagination.Id).AsQueryable();

	        if (!string.IsNullOrWhiteSpace(pagination.Filter))
		        consulta = consulta.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));

			var totalCities = await consulta.CountAsync();
	        var totalPages = (int)Math.Ceiling((double)totalCities / pagination.Records);
	        return Ok(totalPages);
        }

		[HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
	        var consulta = await _context.Cities.FirstOrDefaultAsync(z=> z.Id == id);
	        if (consulta == null) return NotFound();
	        return Ok(consulta);
        }

		[HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody]City city)
        {
            try
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
	            return BadRequest(ex.GetExceptionMessage());
            }
		}

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody]City city)
        {
	        try
	        {
				_context.Update(city);
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
	        var consulta = await _context.Cities.FirstOrDefaultAsync(z => z.Id == id);
	        if (consulta == null) return NotFound();

			_context.Remove(consulta);
	        await _context.SaveChangesAsync();
	        return Ok();
        }
	}
}
