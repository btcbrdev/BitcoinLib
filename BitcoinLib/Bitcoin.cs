using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinLib.BlockchainAPI;

namespace BitcoinLib
{
    public class Bitcoin
    {
        /// <summary>
        /// All bitcoin values are in Satoshi i.e. divide by 100000000 to get the amount in BTC.
        /// --
        /// https://blockchain.info/api/blockchain_wallet_api
        /// </summary>

        public const double _btcDivide = 100000000;


        /// <summary>
        /// Convert Btc to decimal format.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string BTCtoDecimal(long val)
        {
            double v = val / _btcDivide;
            return v.ToString();
        }


        /// <summary>
        /// Convert decimal to Btc format.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long DecimalToBTC(double? val)
        {
            if (val <= 0)
                return 0;

            long v = Convert.ToInt64(val * _btcDivide);
            return v;
        }

        /// <summary>
        /// Transactions confirmations
        /// </summary>
        /// <param name="TxHash">Transaction hash or Transaction ID</param>
        /// <returns>Returns the amount of transaction confirmation.</returns>
        public static TxStatus TxConfirmation(string TxHash)
        {
            return BlockExplorer.TxConfirmation(TxHash);
        }
    }
}
