
namespace Data.Models;

public class PricesCreateRequest
{
    public string? Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}
