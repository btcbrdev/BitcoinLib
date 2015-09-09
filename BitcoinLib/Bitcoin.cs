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

        public static string BTCtoDecimalFormat(long val)
        {
            double v = val / _btcDivide;
            return v.ToString();
        }

        public static long DecimalFormatToBTC(double? val)
        {
            if (val <= 0)
                return 0;

            long v = Convert.ToInt64(val * _btcDivide);
            return v;
        }
    }
}
