using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib
{
    public class Bitcoin
    {
        /// <summary>
        /// All bitcoin values are in Satoshi i.e. divide by 100000000 to get the amount in BTC.
        /// https://blockchain.info/api/blockchain_wallet_api
        /// </summary>
        public const double _btcDivide = 100000000;

        public static string displayDecimalFormat(long val)
        {
            double v = val / _btcDivide;
            return v.ToString();
        }
    }
}
