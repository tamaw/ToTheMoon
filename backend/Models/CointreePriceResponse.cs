using System;

namespace ToTheMoon.Api.Models
{
    public class CointreePriceResponse
    {
        public string Buy { get; set; }
        public string Sell { get; set; }
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public decimal Rate { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}