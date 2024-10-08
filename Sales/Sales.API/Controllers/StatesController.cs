﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API._Helpers;
using Sales.API.Data;
using Sales.Shared.DTO;
using Sales.Shared.Entidades;

namespace Sales.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/states")]
    public class StatesController(AppDbContext _context) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
	        var consulta = _context.States.Where(z => z.CountryId == pagination.Id).AsQueryable();

	        if (!string.IsNullOrWhiteSpace(pagination.Filter))
		        consulta = consulta.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));

	        return Ok(await consulta.Include(z => z.Cities!)
		        .OrderBy(x => x.Name)
		        .Paginate(pagination)
		        .ToListAsync());
        }

        [HttpGet("totalpages")]
        public async Task<ActionResult<int>> GetPages([FromQuery] PaginationDTO pagination)
        {
	        var consulta = _context.States.Where(z => z.CountryId == pagination.Id).AsQueryable();

	        if (!string.IsNullOrWhiteSpace(pagination.Filter))
		        consulta = consulta.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));

			var totalStates = await consulta.CountAsync();
	        var totalPages = (int)Math.Ceiling((double)totalStates / pagination.Records);
	        return Ok(totalPages);
        }

        [AllowAnonymous]
        [HttpGet("combo/{CountryId:int}")]
        public async Task<IActionResult> GetComboAsync(int CountryId)
        {
	        return Ok(await _context.States.Where(z=> z.CountryId == CountryId).ToListAsync());
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
