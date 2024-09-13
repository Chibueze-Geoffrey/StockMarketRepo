using Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockMarketRepo.DTOs.Comment;

namespace Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDb _context;
        public CommentRepository(ApplicationDb context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto)
        {
            var CommentExists = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (CommentExists == null)
            {
                return null;
            }

            CommentExists.Title = commentDto.Title;
            CommentExists.Content = commentDto.Content;

            await _context.SaveChangesAsync();
            return CommentExists;
        }
        public async Task<Comment?> DeleteAsync(int id)
        {
            var model = await _context.Comments.FirstOrDefaultAsync(x=>x.Id == id);
            if(model == null)
            {
                return null;
            }
            _context.Comments.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}