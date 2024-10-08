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
    [Route("api/countries")]
    public class CountriesController(AppDbContext _context) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
	        var consulta = _context.Countries.AsQueryable();

	        if (!string.IsNullOrWhiteSpace(pagination.Filter))	
		        consulta = consulta.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));	
		        
			return Ok(await consulta
				.Include(z => z.States)
				.OrderBy(x => x.Name)
				.Paginate(pagination)
				.ToListAsync());
        }

        [HttpGet("totalpages")]
        public async Task<ActionResult<int>> GetPages([FromQuery] PaginationDTO pagination)
        {
	        var consulta = _context.Countries.AsQueryable();

	        if (!string.IsNullOrWhiteSpace(pagination.Filter))
		        consulta = consulta.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));

			var totalCountries = await consulta.CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalCountries / pagination.Records);
			return Ok(totalPages);
        }

		[AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
	        return Ok(await _context.Countries.ToListAsync());
        }


		[HttpGet("listfull")]
        public async Task<IActionResult> GetFullAsync()
        {
	        var consulta = await _context.Countries
		        .Include(z => z.States!)
		        .ToListAsync();
	        return Ok(consulta);
        }

		[HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
	        var consulta = await _context.Countries.FirstOrDefaultAsync(z=> z.Id == id);
	        if (consulta == null) return NotFound();
	        return Ok(consulta);
        }

		[HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody]Country country)
        {
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.GetExceptionMessage());
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Country country)
        {
	        try
	        {
				_context.Update(country);
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
	        var consulta = await _context.Countries.FirstOrDefaultAsync(z => z.Id == id);
	        if (consulta == null) return NotFound();

			_context.Remove(consulta);
	        await _context.SaveChangesAsync();
	        return Ok();
        }
	}
}
