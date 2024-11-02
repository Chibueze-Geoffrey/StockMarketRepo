using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarketRepo.Helpers;


[Route("Api/stock")]
[ApiController]

public class StockController : ControllerBase
{
    private readonly ApplicationDb _context;
    private readonly IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepository, ApplicationDb context)
    {
        _stockRepo = stockRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        var stock = await _stockRepo.GetAllAsync(query);
        var stockDto = stock.Select(x =>x.stockDto());
        return Ok(stock);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepo.GetByIdAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.stockDto());
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto StockRequestDto)
    {
        var stockModel = StockRequestDto.StockCreated();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { Id = stockModel.Id },
        stockModel.stockDto());
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var StockToUpdate = await _stockRepo.UpdateAsync(id, updateDto);
        if (StockToUpdate == null)
        {
            return NotFound();
        }

        await _context.SaveChangesAsync();

        return Ok(StockToUpdate.stockDto());
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockToDelete = await _stockRepo.DeleteAsync(id);
        if (stockToDelete == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}