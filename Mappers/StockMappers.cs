public static class StockMappers
{
    public static StockDto stockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            Div = stockModel.Div,
            Industry=stockModel.Industry,
            MarketCap=stockModel.MarketCap
        };
    }
    public static Stock StockCreated(this CreateStockRequestDto createStockRequestDto )
    {
        return new Stock
        {
            Symbol= createStockRequestDto.Symbol,
            CompanyName = createStockRequestDto.CompanyName,
            Purchase = createStockRequestDto.Purchase,
            Div = createStockRequestDto.Div,
            Industry = createStockRequestDto.Industry,
            MarketCap = createStockRequestDto.MarketCap
        };
    }
}