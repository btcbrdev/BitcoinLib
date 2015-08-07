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
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbnWalletAddr_Click(object sender, RoutedEventArgs e)
        {
            var w = new BitcoinLib.BlockchainAPI.Wallet(tbGUID.Text, tbPassword.Password, tb2Password.Text, tbApiCode.Text);

            try
            {
                foreach (var x in w.getWalletAddress())
                {
                    tbMemo.Text += String.Format("{0} - address: {1} Balance: {2} Total Received: {3} Label: {4}\r\n", DateTime.Now.ToString(), x.AddressStr, x.Balance, x.TotalReceived, x.Label);
                }
            }
            catch (Exception ex)
            {
                tbMemo.Text += String.Format("\r\n{0} - ERROR: {1}\r\n", DateTime.Now.ToString(), ex.Message);
            }
        }
    }
}
