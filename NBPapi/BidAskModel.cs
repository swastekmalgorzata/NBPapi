namespace NBPapi
{
    public class AskSellCurrency
    {
        public class Rate2
        {
            public string currency { get; set; }
            public string code { get; set; }
            public double bid { get; set; }
            public double ask { get; set; }
        }

        public class Root2
        {
            public string table { get; set; }
            public string no { get; set; }
            public string tradingDate { get; set; }
            public string effectiveDate { get; set; }
            public List<Rate2> rates { get; set; }
        }
    }
}

