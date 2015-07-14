using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Info.Blockchain.API;
using Info.Blockchain.API.ExchangeRates;

namespace BitcoinLib.BlockchainAPI
{
    /// <summary>
    /// API Doc
    /// https://blockchain.info/api/exchange_rates_api
    /// </summary>
    public class ExchangeRates
    {
        private string _apiCode { get; set; }

        private Dictionary<string, Currency> __Tickers { get; set; }

        public ExchangeRates(string apiCode = null)
        {
            HttpClient.TimeoutMs = 20000; //Default = 10000
            _apiCode = apiCode;
        }

        private Dictionary<string, Currency> Tickers
        {
            get
            {
                if (__Tickers == null)
                {
                    try
                    {
                        var xTck = Info.Blockchain.API.ExchangeRates.ExchangeRates.GetTicker(_apiCode);
                        __Tickers = xTck;
                        return __Tickers;
                    }
                    catch (APIException APIe)
                    {
                        throw (APIe);
                    }
                }
                else
                    return __Tickers;
            }
        }

        /// <summary>
        /// Return list of all tickers.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Currency> getTicker()
        {
            return Tickers;
        }
        
        /// <summary>
        /// Get the value of Blockchain the ticker.
        /// </summary>
        /// <param name="currency">currency.</param>
        public Currency getBtcTicker(string currency = "USD")
        {
            var xKey = Tickers.Where(k => k.Key == currency).FirstOrDefault();
            return xKey.Value;
        }

        /// <summary>
        /// Returns the value converted into Bitcoin.
        /// </summary>
        /// <param name="currency"></param>
        public double getToBtc(double value = 1.0, string currency = "USD")
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            double Val = Info.Blockchain.API.ExchangeRates.ExchangeRates.ToBTC(currency, value, _apiCode);
            return Val;
        }

        /// <summary>
        /// Returns the value in the country's currency.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="value"></param>
        public double getCurrencyCodeToBtc(string currency, double value)
        {
            double Val = Info.Blockchain.API.ExchangeRates.ExchangeRates.ToBTC(currency, value, _apiCode);
            return Val;
        }
    }
}
