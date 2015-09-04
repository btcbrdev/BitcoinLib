using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Info.Blockchain;

namespace Bitcoin_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://github.com/blockchain/api-v1-client-csharp
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WriteText(string ss)
        {
            tbMemo.Text += $"\r\n{ss}\r\n";
            tbMemo.ScrollToEnd();
        }

        private void tbnWalletAddr_Click(object sender, RoutedEventArgs e)
        {
            var w = new BitcoinLib.BlockchainAPI.Wallet(tbGUID.Text, tbPassword.Password, tb2Password.Text, tbApiCode.Text);

            try
            {
                string tx = "";
                foreach (var x in w.getWalletAddress())
                {
                    tx += ($"{DateTime.Now.ToString()} - {x.AddressStr} Balance: {Convert.ToString(x.Balance).PadRight(11)} T. Received: {Convert.ToString(x.TotalReceived).PadRight(14)} Label: {x.Label}\r\n");
                }

                WriteText(tx);
            }
            catch (Exception ex)
            {
                WriteText($"{DateTime.Now.ToString()} - ERROR: {ex.Message}");
            }
        }

        private void btnTicker_Click(object sender, RoutedEventArgs e)
        {
            var x = new BitcoinLib.BlockchainAPI.ExchangeRates();
            var c = x.getTickers();
            string s = "";

            foreach (var item in c.Values.ToList())
            {
                s += $"symbol: {item.Symbol.PadRight(5)} price 15m: {item.Price15m} last:{item.Last} buy: {item.Buy} sell: {item.Sell}\r\n";
            }

            WriteText(s);
        }
    }
}
