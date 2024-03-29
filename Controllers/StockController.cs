using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Data;
using dotnet8_api.Dtos.Stock;
using dotnet8_api.Helpers;
using dotnet8_api.Interfaces;
using dotnet8_api.Mappers;
using dotnet8_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet8_api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {   
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
                
            var stocks = await _stockRepository.GetAllAsync(query);
            var stockDto =  stocks.Select(s => s.ToStockDto()).ToList();

            return Ok(stockDto);
        }   

        [HttpGet("{stockId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int stockId) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) 
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stockModel = stockDto.ToStockFromCreateDto();
            await _stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{stockId:int}")]
        public async Task<IActionResult> Update([FromRoute] int stockId, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stockModel = await _stockRepository.UpdateAsync(stockId, updateDto);

            if (stockModel == null) 
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{stockId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int stockId) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stockModel = await _stockRepository.DeleteAsync(stockId);

            if (stockModel == null) 
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}