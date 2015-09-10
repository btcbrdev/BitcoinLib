using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Info.Blockchain.API.Receive;

namespace BitcoinLib.BlockchainAPI
{
    /// <summary>
    /// https://blockchain.info/api/api_receive
    /// </summary>
    public class Receive
    {
        /// <summary>
        /// This method creates a unique address which should be presented to the customer. Any payments sent to this address will be forwarded to your own bitcoin address. Each time a payment is forwarded the callback URL will be called.
        /// </summary>
        /// <param name="ReceiveAddress">Your Receiving Bitcoin Address.</param>
        /// <param name="CallBackURL">The callback URL to be notified when a payment is received. Remember to URL.</param>
        /// <param name="apiCode">API Code.</param>
        /// <returns></returns>
        public static ReceiveResponse ReceivePayments(string ReceiveAddress, string CallBackURL, string apiCode = null)
        {
            return Info.Blockchain.API.Receive.Receive.ReceiveFunds(ReceiveAddress, CallBackURL, apiCode);
        }
    }
}
