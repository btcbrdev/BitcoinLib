using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib.BlockchainAPI
{
    /// <summary>
    /// Get status of transaction (Confirmation, Double spend)
    /// </summary>
    public class TxStatus
    {
        public long Confirmation { get; private set; }
        public bool DoubleSpend { get; private set; }

        public TxStatus(long? txConf, bool? DSpend)
        {
            Confirmation = txConf.Value;
            DoubleSpend = DSpend.Value;
        }
    }

    /// <summary>
    /// API Doc
    /// https://blockchain.info/api/blockchain_api
    /// --
    /// https://github.com/blockchain/api-v1-client-csharp/blob/master/docs/blockexplorer.md
    /// </summary>
    public class BlockExplorer : Info.Blockchain.API.BlockExplorer.BlockExplorer
    {
        public static TxStatus TxConfirmation(string TxHash)
        {
            if (string.IsNullOrWhiteSpace(TxHash.Trim()))
                throw new ArgumentException("Undefined or Null argument are not valid!", nameof(TxHash));

            var BApiEx = new Info.Blockchain.API.BlockExplorer.BlockExplorer();
            var tx = BApiEx.GetTransaction(TxHash);
            var LBtx = BApiEx.GetLatestBlock();

            var TxConf = new TxStatus((LBtx?.Height - tx?.BlockHeight + 1), tx.DoubleSpend);
            return TxConf;
        }
    }
}
