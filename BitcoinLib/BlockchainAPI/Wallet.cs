using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Info.Blockchain.API.Wallet;


namespace BitcoinLib.BlockchainAPI
{
    /// <summary>
    /// https://blockchain.info/api/create_wallet
    /// </summary>

    public class Wallet
    {
        private string _guid { get; set; }
        private string _password { get; set; }
        private string _secounfPassword { get; set; }
        private string _apiCode { get; set; }

        private Info.Blockchain.API.Wallet.Wallet BCw { get; }

        /// <summary>
        /// Wallet class that reflects the functionality documented at at https://blockchain.info/api/blockchain_wallet_api. It allows users to directly interact with their existing Blockchain.info wallet, send funds, manage addresses etc.
        /// </summary>
        /// <param name="guid">Your wallet guid.</param>
        /// <param name="password">Your wallet password.</param>
        /// <param name="secoundPassword">Secoud password.</param>
        /// <param name="apiCod">API Code.</param>
        public Wallet(string guid, string password, string secoundPassword = null, string apiCod = null)
        {
            _guid = guid;
            _password = password;
            _secounfPassword = secoundPassword;
            _apiCode = apiCod;
            BCw = new Info.Blockchain.API.Wallet.Wallet(_guid, _password, _secounfPassword, _apiCode);
        }

        /// <summary>
        /// Send bitcoin from your wallet to another bitcoin address. All transactions include a 0.0001 BTC miners fee if not set.
        /// </summary>
        /// <param name="toAddress">Recipient Bitcoin Address.</param>
        /// <param name="amount">Amount to send in satoshi.</param>
        /// <param name="fromAddress">Send from a specific Bitcoin Address (Optional)</param>
        /// <param name="fee">Transaction fee value in satoshi (Must be greater than default fee) (Optional)</param>
        /// <param name="note">A public note to include with the transaction. Can only be attached when outputs are greater than 0.005 BTC. (Optional)</param>
        /// <returns>{ "message" : "Response Message" , "tx_hash": "Transaction Hash", "notice" : "Additional Message" }</returns>
        public PaymentResponse Send(string toAddress, long amount, string fromAddress = null, long? fee = default(long?), string note = null)
        {
            return BCw.Send(toAddress, amount, fromAddress, fee, note);
        }


        /// <summary>
        /// List all active addresses in a wallet. Also includes a 0 confirmation balance which should be used as an estimate only and will include unconfirmed transactions and possibly double spends.
        /// </summary>
        /// <param name="confirmation">Minimum number of confirmations required. 0 for unconfirmed.</param>
        /// <returns>List of addresses</returns>
        public List<Address> getWalletAddress(int confirmation = 0)
        {
            return BCw.ListAddresses(confirmation);
        }

        /// <summary>
        /// Generating a new address.
        /// </summary>
        /// <param name="label">An optional label to attach to this address. It is recommended this is a human readable string e.g. "Order No : 1234". You May use this as a reference to check balance of an order.</param>
        /// <returns></returns>
        public Address NewAddress(string label = null)
        {
            return BCw.NewAddress(label);
        }

        /// <summary>
        /// Get balance of all wallet address.
        /// </summary>
        /// <returns>Ballance</returns>
        public long WalletBalance()
        {
            return BCw.GetBalance();
        }

        /// <summary>
        /// Retrieve the balance of a bitcoin address.
        /// </summary>
        /// <param name="adr">The bitcoin address to lookup.</param>
        /// <param name="confirmation">Minimum number of confirmations required. 0 for unconfirmed.</param>
        /// <returns></returns>
        public Address getAddressBallance(string adr, int confirmation = 0)
        {
            return getWalletAddress(confirmation).Where(x => x.AddressStr == adr).FirstOrDefault();
        }

        /// <summary>
        /// To improve wallet performance addresses which have not been used recently should be moved to an archived state. They will still be held in the wallet but will no longer be included in the "list" or "list-transactions" calls.
        /// </summary>
        /// <param name="adr">Bitcoin adrress</param>
        /// <returns>{"archived" : "address"}</returns>
        public string ArchiveAddress(string adr)
        {
            return BCw.ArchiveAddress(adr);
        }

        /// <summary>
        /// Unarchive an address. Will also restore consolidated addresses.
        /// </summary>
        /// <param name="adr">Bitcoin adrress</param>
        /// <returns>{"active" : "address"}</returns>
        public string UnArchiveAdrres(string adr)
        {
            return BCw.UnarchiveAddress(adr);
        }

        /// <summary>
        /// Queries to wallets with over 10 thousand addresses will become sluggish especially in the web interface. The auto_consolidate command will remove some inactive archived addresses from the wallet and insert them as forwarding addresses (see receive payments API). You will still receive callback notifications for these addresses however they will no longer be part of the main wallet and will be stored server side.
        /// </summary>
        /// <param name="Days">A good value for days is 60 i.e. addresses which have not received transactions in the last 60 days will be consolidated.</param>
        /// <returns>List of address</returns>
        public List<string> Consolidate(int Days = 0)
        {
            return BCw.Consolidate(Days);
        }
    }
}
