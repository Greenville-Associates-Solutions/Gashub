using System;

namespace Gas.Models
{
public partial class GasTickerPrice
{
    public int Id { get; set; }              // EF primary key
    public string GasTicker { get; set; }
    public DateTime RecordDate { get; set; } // only the Date part is used
    public decimal Price { get; set; }
    public string Description { get; set; }
}
}