using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinLib
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
}
