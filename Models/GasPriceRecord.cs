using System;
using System.Collections.Generic;

namespace Gas.Models;

public partial class GasPriceRecord
{
    public int Id { get; set; }

    public DateTime RecordDate { get; set; }

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

    public decimal? Price1 { get; set; }

    public decimal? Price2 { get; set; }

    public decimal? Price3 { get; set; }

    public decimal? Price4 { get; set; }

    public decimal? Price5 { get; set; }

    public decimal? Price6 { get; set; }

    public decimal? Price7 { get; set; }

    public decimal? Price8 { get; set; }

    public decimal? Price9 { get; set; }

    public decimal? Price10 { get; set; }

    public decimal? DailyAverage { get; set; }

    public int? TickerTotals { get; set; }

    public string GasHubId { get; set; }

    public string Description { get; set; }
}
