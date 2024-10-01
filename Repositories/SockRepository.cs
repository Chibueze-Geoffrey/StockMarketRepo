using Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockMarketRepo.Helpers;

namespace Api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDb _context;
        public StockRepository(ApplicationDb context)
        {
            _context = context;

        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var deleteStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (deleteStock == null)
            {
                Console.WriteLine("No Stocks with the Id Exists!");
                return null;
            }
            _context.Stocks.Remove(deleteStock);
            await _context.SaveChangesAsync();
            return deleteStock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c=>c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s=>s.CompanyName.Contains(query.CompanyName));
            }
              if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s=>s.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks= query.IsDescending? stocks.OrderByDescending(s=>s.Symbol) 
                        : stocks.OrderBy(s=>s.Symbol);
                }
            }

           
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public Task<bool> stockExistsAsync(int id)
        {
            return _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var StockExist = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (StockExist == null)
            {
                Console.WriteLine("No Stocks with the Id Exists!");
                return null;
            }

            StockExist.Symbol = stockDto.Symbol;
            StockExist.CompanyName = stockDto.CompanyName;
            StockExist.Div = stockDto.Div;
            StockExist.Industry = stockDto.Industry;
            StockExist.Purchase = stockDto.Purchase;
            StockExist.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return StockExist;

        }
    }
}