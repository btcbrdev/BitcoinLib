using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Info.Blockchain.API.ExchangeRates;
using BitcoinLib.BlockchainAPI;

namespace BitcoinLib.BRL
{
    public static class Util_BRL
    {
        const string currencySymbolBRL = "BRL";

        /// <summary>
        /// Pega o ticker da Blockchain com o valor em BRL.
        /// </summary>
        /// <param name="apiCode">Código da API.</param>
        /// <returns>Retorna Buy, Last, Price15m, Sell, Symbol </returns>
        public static Currency getBtcBRLTicker(string apiCode = null)
        {
            var _exR = new BitcoinLib.BlockchainAPI.ExchangeRates(apiCode);
            return _exR.getBtcTicker(currencySymbolBRL);
        }
        
        /// <summary>
        /// Retorna o valor em R$ convertido para Bitcoin.
        /// </summary>
        /// <param name="valor">Valor a ser convertido em Bitcoin.</param>
        /// <param name="apiCode">Cádigo da API.</param>
        public static double getBRLtoBtc(double valor = 1.00, string apiCode = null)
        {
            var _exR = new BitcoinLib.BlockchainAPI.ExchangeRates(apiCode);
            return _exR.getToBtc(valor, currencySymbolBRL);
        }
    }
}
