using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
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