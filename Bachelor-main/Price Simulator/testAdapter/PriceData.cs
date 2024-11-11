namespace Price_Simulator.DataModels;

public record PriceData
{
    public string Id { get; set; }
    /// <summary>
    /// Last trade price. Not-a-number if there has not been a trade yet.
    /// </summary>
    public double TradePrice { get; set; }
    /// <summary>
    /// The volume of the last trade. Not-a-number if there was no trade yet
    /// </summary>
    public double TradeVolume { get; set; }
    /// <summary>
    /// Date and time of the the last trade, in microseconds since the Unix epoch.
    /// Undefined if there was no trade ever. This date may point to a day before today.
    /// </summary>
    public long TradeDate { get; set; }
    /// <summary>
    /// Accumulated number of shares, lots or contracts traded during the current trading day.
    /// Zero if there was no trade yet.
    /// </summary>
    public double AccumulatedVolume { get; set; }
    /// <summary>
    /// Highest trade price of the current trading day. Not-a-number if there was no trade the current trading day.
    /// </summary>
    public double HighPrice { get; set; }
    /// <summary>
    /// Lowest trade price of the current trading day. Not-a-number if there was no trade the current trading day.
    /// </summary>
    public double LowPrice { get; set; }
    /// <summary>
    /// The number of trades of the current trading day. For indices, the number of times the index has been calculated.
    /// </summary>
    public int NumberOfTrades { get; set; }

}
