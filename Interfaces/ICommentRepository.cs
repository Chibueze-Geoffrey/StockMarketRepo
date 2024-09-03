namespace Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> GetByIdAsync(int id);

    }
}