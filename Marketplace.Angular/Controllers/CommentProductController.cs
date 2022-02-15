using AutoMapper;
using Marketplace.Angular.Models;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Angular.Controllers
{
    [Route("")]
    [ApiController]
    public class CommentProductController : Controller
    {
        private readonly ICommentProductService _commentService;
        private readonly IMapper _mapper;

        public CommentProductController(
            ICommentProductService commentService,
            IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet("get-comment-all")]
        public async Task<IActionResult> GetCommentsAll()
        {
            var comments = await _commentService.GetAllAsync();
            IActionResult result = comments == null ? NotFound() : Ok(_mapper.Map<List<CommentProductVM>>(comments));

            return result;
        }

        //[Authorize]
        [HttpGet("get-comment-by-id/{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var comments = await _commentService.GetByIdAsync(id);
            var commentsResult = _mapper.Map<CommentProductVM>(comments);
            IActionResult result = comments == null ? NotFound() : Ok(commentsResult);

            return result;
        }

        [HttpPost("add-comment")]
        public async Task<IActionResult> AddAsync(CommentProductVM comment)
        {
            await _commentService.AddAsync(_mapper.Map<CommentProductDto>(comment));

            return Ok(true);
        }

        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateAsync(CommentProductVM comment)
        {
            await _commentService.UpdateAsync(_mapper.Map<CommentProductDto>(comment));

            return Ok(true);
        }

        [HttpDelete("delete-comment/{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _commentService.DeleteAsync(id);

            return Ok(true);
        }
        
    }
}
