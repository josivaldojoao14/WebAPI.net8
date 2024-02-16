using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Dtos.Stock;
using dotnet8_api.Extensions;
using dotnet8_api.Interfaces;
using dotnet8_api.Mappers;
using dotnet8_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet8_api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository,
            UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllAsync();
            var commentsDto =  comments.Select(s => s.ToCommentDto()).ToList();

            return Ok(commentsDto);
        }   

        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int commentId) 
        {   
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(commentId);

            if (comment == null) 
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{commentId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            if (!await _stockRepository.StockExists(stockId)) 
            {
                return BadRequest("Stock doesn't exist");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            commentModel.AppUserId = appUser.Id;

            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { stockId = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{commentId:int}")]
        public async Task<IActionResult> Update([FromRoute] int stockId, [FromBody] UpdateCommentDto commentDto)
        {   
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var comment = await _commentRepository.UpdateAsync(stockId, commentDto.ToCommentFromUpdate());

            if (comment == null) 
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{commentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int stockId) 
        {   
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
                
            var comment = await _commentRepository.DeleteAsync(stockId);

            if (comment == null) 
            {
                return NotFound("Comment not found");
            }

            return NoContent();
        }
    }
}