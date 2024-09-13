using StockMarketRepo.DTOs.Comment;

namespace Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
        Task <Comment?> DeleteAsync(int id);

    }
}