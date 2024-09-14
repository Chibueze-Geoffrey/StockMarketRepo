using System.ComponentModel.DataAnnotations;

public class UpdateStockRequestDto
{
     [Required]
    public string Symbol { get; set; } = string.Empty;
     [Required]
    public string CompanyName { get; set; } = string.Empty;
     [Required]
     [Range(1,1000000)]
    public decimal Purchase { get; set; }
     [Required]
     [Range(0.001,100)]
    public decimal Div { get; set; }
     [Required]
    public string Industry { get; set; } = string.Empty;
     [Required]
    public long MarketCap { get; set; }
}