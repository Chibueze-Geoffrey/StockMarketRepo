using Api.DTOs.Comment;
using Api.Interfaces;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using StockMarketRepo.DTOs.Comment;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepo = commentRepository;
            _stockRepo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comment = await _commentRepo.GetAllAsync();
            var commentDto = comment.Select(x => x.commentDto());
            return Ok(comment);
        }

        [HttpGet("{id: int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.commentDto());
        }

        [HttpPost("{stockId: int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,
        [FromBody] CreateCommentRequestDto commentRequestDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Opps");
            }
            if (!await _stockRepo.stockExistsAsync(stockId))
            {
                return BadRequest("Stock Not Found!!");
            }
            var commentCreated = commentRequestDto.CommentCreated(stockId);
            await _commentRepo.CreateAsync(commentCreated);
            return CreatedAtAction(nameof(GetById), new { id = commentCreated }, commentCreated.commentDto());
        }
        [HttpPut("{id: int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            var updateComment = await _commentRepo.UpdateAsync(id, commentDto);
            if (updateComment == null)
            {
                return NotFound();
            }

            return Ok(updateComment.commentDto());
        }
        [HttpDelete("{id: int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentDelete = await _commentRepo.DeleteAsync(id);
            if (commentDelete == null)
            {
                return NotFound("Does Not Exists!!");
            }
            return Ok(commentDelete);
        }
    }
}