
using System;
using System.Runtime.InteropServices;
using Adapter.DataModels;
using Price_Simulator.Adapter;

namespace Price_Simulator.Price_Engine;

public class PriceEventFactory : IPriceEventFactory
{
    private PriceData[] _prices = new PriceData[200];

    private Random _random = new();

    private int _priceCount;

    private TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);

    public PriceEvent Create()
    {
        return new PriceEvent()
        {
            PriceData = new EnrichedPriceData(){CpuUsage = null, Price = CreatePriceData(), RamUsage = null},
            PriceId = Guid.NewGuid().ToString()
        };
    }

    public PriceData CreatePriceData()
    {
        if (_priceCount >=200)
        {
            return UpdatePrice(_prices[_random.Next(199)]);
        }

        var price = new PriceData()
        {
            AccumulatedVolume = _random.Next(2000, 2500),
            HighPrice = _random.Next(200, 400),
            Id = RandomString(12),
            LowPrice = _random.Next(50, 100),
            NumberOfTrades = _random.Next(1500, 15000),
            TradeDate = (long)t.Seconds,
            TradePrice = _random.Next(100,150),
            TradeVolume = _random.Next(1000000, 100000000)
        };

        _prices[_priceCount] = price;
        _priceCount++;
        return price;
    }

    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    private PriceData UpdatePrice(PriceData price)
    {
        price.TradePrice += _random.Next(4);
        if (price.TradePrice > 0)
        {
            price.HighPrice = Math.Max(price.HighPrice, price.TradePrice);
            price.LowPrice = price.LowPrice == 0 ? price.TradePrice : Math.Min(price.LowPrice, price.TradePrice);
        }

        // Assuming some trade occurred
        price.NumberOfTrades += 1;
        price.AccumulatedVolume += price.TradeVolume;

        // Return the updated price data
        return price;
    }
}