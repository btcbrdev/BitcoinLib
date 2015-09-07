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
using BitcoinLib;

namespace Bitcoin_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://github.com/blockchain/api-v1-client-csharp
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitcoinLib.BlockchainAPI.Wallet WL
        {
            get
            {
                return new BitcoinLib.BlockchainAPI.Wallet(tbGUID.Text, tbPassword.Password, tb2Password.Password, tbApiCode.Text);
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        private void WriteText(string ss)
        {
            tbMemo.Text += $"\r\n{ss}\r\n";
            tbMemo.ScrollToEnd();
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

        private void tbnWalletAddr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tx = "";
                WriteText("--- Minimal 0 confirmatio ---");
                foreach (var x in WL.getWalletAddress())
                {
                    tx += ($"{DateTime.Now.ToString()} - {x.AddressStr} Balance: {Bitcoin.displayDecimalFormat(x.Balance)} T. Received: {Bitcoin.displayDecimalFormat(x.TotalReceived)} Label: {x.Label}\r\n");
                }
                WriteText(tx);

                tx = "";
                WriteText("--- Minimal 12 confirmatio ---");
                foreach (var x in WL.getWalletAddress(12))
                {
                    tx += ($"{DateTime.Now.ToString()} - {x.AddressStr} Balance: {Bitcoin.displayDecimalFormat(x.Balance)} T. Received: {Bitcoin.displayDecimalFormat(x.TotalReceived)} Label: {x.Label}\r\n");
                }
                WriteText(tx);
            }
            catch (Exception ex)
            {
                WriteText($"{DateTime.Now.ToString()} - ERROR: {ex.Message}");
            }
        }

        private void tbnWalletBallance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteText($"{DateTime.Now.ToString()} - Wallet Balance ฿{Bitcoin.displayDecimalFormat(WL.WalletBalance())}");
            }
            catch (Exception ex)
            {
                WriteText($"{DateTime.Now.ToString()} - ERROR: {ex.Message}");
            }
        }

        private void btnAddressBallance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteText("--- Minimal 0 confirmatio ---");
                var ad = WL.getAddressBallance(tbAddress.Text);
                WriteText($"{DateTime.Now.ToString()} - Address: {ad.AddressStr} - Ballance ฿{Bitcoin.displayDecimalFormat(ad.Balance)} - Total Received: {Bitcoin.displayDecimalFormat(ad.TotalReceived)} -Label: {ad.Label}");

                WriteText("--- Minimal 12 confirmatio ---");
                ad = WL.getAddressBallance(tbAddress.Text, 12);
                WriteText($"{DateTime.Now.ToString()} - Address: {ad.AddressStr} - Ballance ฿{Bitcoin.displayDecimalFormat(ad.Balance)} - Total Received: {Bitcoin.displayDecimalFormat(ad.TotalReceived)} -Label: {ad.Label}");
            }
            catch (Exception ex)
            {
                WriteText($"{DateTime.Now.ToString()} - ERROR: {ex.Message}");
            }
        }

        private void btnNewAddress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ad = WL.NewAddress($"test in {DateTime.Now.ToString()}");
                WriteText($"{DateTime.Now.ToString()} - Address: {ad.AddressStr} - Ballance ฿{Bitcoin.displayDecimalFormat(ad.Balance)} - Total Received: {Bitcoin.displayDecimalFormat(ad.TotalReceived)} -Label: {ad.Label}");
            }
            catch (Exception ex)
            {
                WriteText($"{DateTime.Now.ToString()} - ERROR: {ex.Message}");
            }
        }

        private void btnArchive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteText($"Archived - {WL.ArchiveAddress(tbAddress.Text)}");
            }
            catch (Exception ex)
            {
                WriteText($"{DateTime.Now.ToString()} - ERROR: {ex.Message}");
            }

        }

        private void btnUnArchive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteText($"Unarchived - {WL.UnArchiveAdrres(tbAddress.Text)}");
            }
            catch (Exception ex)
            {
                WriteText($"{DateTime.Now.ToString()} - ERROR: {ex.Message}");
            }
        }
    }
}
