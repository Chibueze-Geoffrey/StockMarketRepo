public class StockDto
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal Div { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }

}