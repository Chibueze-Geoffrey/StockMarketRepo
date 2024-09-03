using Microsoft.EntityFrameworkCore;

public class ApplicationDb : DbContext
{
    public ApplicationDb(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Stock> Stocks { get; set; }


}