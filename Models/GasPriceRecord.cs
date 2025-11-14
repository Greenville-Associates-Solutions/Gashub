using System;
using System.ComponentModel.DataAnnotations;

namespace GasHub.Models
{
    public class GasPriceRecord
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        // Tickers
        public string Ticker1 { get; set; }
        public string Ticker2 { get; set; }
        public string Ticker3 { get; set; }
        public string Ticker4 { get; set; }
        public string Ticker5 { get; set; }
        public string Ticker6 { get; set; }
        public string Ticker7 { get; set; }
        public string Ticker8 { get; set; }
        public string Ticker9 { get; set; }
        public string Ticker10 { get; set; }

        // Prices
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Price4 { get; set; }
        public decimal Price5 { get; set; }
        public decimal Price6 { get; set; }
        public decimal Price7 { get; set; }
        public decimal Price8 { get; set; }
        public decimal Price9 { get; set; }
        public decimal Price10 { get; set; }
    }
}