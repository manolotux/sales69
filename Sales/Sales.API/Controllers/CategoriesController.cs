using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entidades;

namespace Sales.API.Controllers
{
	[ApiController]
	[Route("api/categories")]
	public class CategoriesController(AppDbContext _context) : Controller
	{
		[HttpGet("list")]
		public async Task<IActionResult> GetAsync()
		{
			var consulta = await _context.Categories.ToListAsync();
			return Ok(consulta);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var consulta = await _context.Categories.FirstOrDefaultAsync(z => z.Id == id);
			if (consulta == null) return NotFound();
			return Ok(consulta);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddAsync([FromBody] Category category)
		{
			try
			{
				_context.Add(category);
				await _context.SaveChangesAsync();
				return Ok();
			}
			catch (DbUpdateException ex)
			{
				if (ex.InnerException?.Message.Contains("duplicate") ?? false)
					return BadRequest("Ya existe una categoria con ese nombre");

				return BadRequest(ex.Message);
			}
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateAsync([FromBody] Category category)
		{
			try
			{
				_context.Update(category);
				await _context.SaveChangesAsync();
				return Ok();
			}
			catch (DbUpdateException ex)
			{
				if (ex.InnerException?.Message.Contains("duplicate") ?? false)
					return BadRequest("Ya existe una categoria con ese nombre");

				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var consulta = await _context.Categories.FirstOrDefaultAsync(z => z.Id == id);
			if (consulta == null) return NotFound();

			_context.Remove(consulta);
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
