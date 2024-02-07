using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Data;
using dotnet8_api.Dtos.Stock;
using dotnet8_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace dotnet8_api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {   
        private readonly ApplicationDbContext _contenxt;
        public StockController(ApplicationDbContext context)
        {
            _contenxt = context;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var stocks = _contenxt.Stocks.ToList()
                .Select(s => s.ToStockDto());

            return Ok(stocks);
        }   

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) 
        {
            var stock = _contenxt.Stocks.Find(id);
            if (stock == null) 
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto) 
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            _contenxt.Add(stockModel);
            _contenxt.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
    }
}