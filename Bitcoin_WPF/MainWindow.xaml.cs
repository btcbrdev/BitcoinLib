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
using BitcoinLib;
using BitcoinLib.BlockchainAPI;
using System.Drawing;
using System.IO;

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
            tbMemo.Text += $"\r\n{DateTime.Now.ToString()}\r\n{ss}\r\n";
            tbMemo.ScrollToEnd();
        }

        private void btnTicker_Click(object sender, RoutedEventArgs e)
        {
            var x = new BitcoinLib.BlockchainAPI.ExchangeRates();
            var c = x.getTickers();
            string s = "";

            foreach (var item in c.Values.ToList())
            {
                s += $"symbol: {item.Symbol.PadRight(5)} price 15m: {item.Price15m.ToString().PadRight(10, ' ')} last: {item.Last.ToString().PadRight(10, ' ')} buy: {item.Buy.ToString().PadRight(10, ' ')} sell: {item.Sell.ToString().PadRight(10, ' ')}\r\n";
            }

            WriteText(s);
        }

        private void tbnWalletAddr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tx = "";
                WriteText("--- Minimal 0 confirmation to show ---");
                foreach (var x in WL.getWalletAddress())
                {
                    tx += ($"{x.AddressStr} Balance: {Bitcoin.BTCtoDecimalFormat(x.Balance)} T. Received: {Bitcoin.BTCtoDecimalFormat(x.TotalReceived)} Label: {x.Label}\r\n");
                }
                WriteText(tx);

                tx = "";
                WriteText("--- Minimal 12 confirmation to show ---");
                foreach (var x in WL.getWalletAddress(12))
                {
                    tx += ($"{x.AddressStr} Balance: {Bitcoin.BTCtoDecimalFormat(x.Balance)} T. Received: {Bitcoin.BTCtoDecimalFormat(x.TotalReceived)} Label: {x.Label}\r\n");
                }
                WriteText(tx);
            }
            catch (Exception ex)
            {
                WriteText($"ERROR: {ex.Message}");
            }
        }

        private void tbnWalletBallance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteText($"Wallet Balance ฿{Bitcoin.BTCtoDecimalFormat(WL.WalletBalance())}");
            }
            catch (Exception ex)
            {
                WriteText($"ERROR: {ex.Message}");
            }
        }

        private void btnAddressBallance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteText("--- Minimal 0 confirmation to show ---");
                var ad = WL.getAddressBallance(tbAddress.Text);
                WriteText($"Address: {ad.AddressStr} - Ballance ฿{Bitcoin.BTCtoDecimalFormat(ad.Balance)} - Total Received: {Bitcoin.BTCtoDecimalFormat(ad.TotalReceived)} -Label: {ad.Label}");

                WriteText("--- Minimal 12 confirmation to show ---");
                ad = WL.getAddressBallance(tbAddress.Text, 12);
                WriteText($"Address: {ad.AddressStr} - Ballance ฿{Bitcoin.BTCtoDecimalFormat(ad.Balance)} - Total Received: {Bitcoin.BTCtoDecimalFormat(ad.TotalReceived)} -Label: {ad.Label}");
            }
            catch (Exception ex)
            {
                if (tbAddress.Text.Trim().Length < 1)
                    WriteText($"ERROR: Please set your wallet address below !");
                else
                    WriteText($"ERROR: {ex.Message}");
            }
        }

        private void btnNewAddress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ad = WL.NewAddress($"BitcoinLib - Creadte address {DateTime.Now.ToString()}");
                WriteText($"Address: {ad.AddressStr} - Ballance ฿{Bitcoin.BTCtoDecimalFormat(ad.Balance)} - Total Received: {Bitcoin.BTCtoDecimalFormat(ad.TotalReceived)} -Label: {ad.Label}");
            }
            catch (Exception ex)
            {
                WriteText($"ERROR: {ex.Message}");
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
                WriteText($"ERROR: {ex.Message}");
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
                WriteText($"ERROR: {ex.Message}");
            }
        }

        private void btnSendBitcoin_Click(object sender, RoutedEventArgs e)
        {
            long amount = 0;
            long? fee = null;
            string fromAdr = null, msg = null;

            try
            {
                amount = Bitcoin.DecimalFormatToBTC(Convert.ToDouble(tbAmount.Text));

                if (tbSendAdr.Text.Trim().Length <= 0)
                    WriteText("Set address to send !");

                if (amount <= 0)
                    WriteText("Set amount more than 0.00000100 !");

                if (tbFromAdr.Text.Trim() != "(optional)")
                    fromAdr = tbFromAdr.Text;

                if (tbMessage.Text != "(optional)")
                    msg = tbMessage.Text;

                try
                {
                    if (tbFee.Text != "(optional)")
                        fee = Bitcoin.DecimalFormatToBTC(Convert.ToDouble(tbFee.Text));
                }
                catch
                {
                    WriteText($"ERROR: Fee value ({tbFee.Text}) is incorrect. Set the minimum to 0.0001");
                    tbFee.Text = "0.0001";
                    return;
                }

                // Send Bitcoins.
                var x = WL.Send(tbSendAdr.Text, amount, fromAdr, fee, msg);

                // If Ok, log transaction hash.
                WriteText($"Your Transaction\r\nHash: {x.TxHash} - Message: {x.Message} - Notice: {x.Notice}");
            }
            catch (Exception ex)
            {
                WriteText($"ERROR: {ex.Message}");
            }
        }

        private void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var R = Receive.ReceivePayments(tbReceiveAdr.Text, tbCallbackUrl.Text);

                WriteText($"Destination: {R.DestinationAddress} - Input Address: {R.InputAddress} - Callback URL: {R.CallbackUrl} - Fee %: {R.FeePercent}");

                tbReceiveInput.Text = R.InputAddress;

                //--=> Generate a QRCode.
                var x = new com.google.zxing.qrcode.QRCodeWriter().encode(R.InputAddress, com.google.zxing.BarcodeFormat.QR_CODE, 200, 200);
                imgQRrec.Source = BitmapToImageSource(x.ToBitmap());
            }
            catch (Exception ex)
            {
                WriteText($"ERROR: {ex.Message}");
            }

        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
