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

        private Info.Blockchain.API.Wallet.Wallet BCw { get; set; }

        /// <summary>
        /// Wallet information.
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
        /// List all active addresses in a wallet. Also includes a 0 confirmation balance which should be used as an estimate only and will include unconfirmed transactions and possibly double spends.
        /// </summary>
        /// <param name="confirmation">Number of confirmation.</param>
        /// <returns>List of addresses</returns>
        public List<Address> getWalletAddress(int confirmation = 0)
        {
            return BCw.ListAddresses(confirmation);
        }
    }
}
