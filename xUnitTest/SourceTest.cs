using System;
using Xunit;
using Xunit.Abstractions;

namespace xUnitTest
{
    // http://xunit.github.io/docs/getting-started.html
    // If need helo to xunit in Visual Studio go to http://xunit.github.io/docs/running-tests-in-vs.html

    public class UnitTest_Util_BRL
    {
        private readonly ITestOutputHelper output;

        public UnitTest_Util_BRL(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void get_BtcBRLTicker()
        {
            var T = BitcoinLib.BRL.Util_BRL.getBtcBRLTicker();

            var j = Newtonsoft.Json.JsonConvert.SerializeObject(T);
            output.WriteLine(j.ToString());

            Assert.IsType(typeof(Info.Blockchain.API.ExchangeRates.Currency), T);
        }

        [Theory]
        [InlineData(1.2)]
        [InlineData(3.55)]
        [InlineData(10.58)]
        [InlineData(1000.239)]
        [InlineData(10123.998)]
        public void get_BRLtoBtc(double val)
        {
            var T = BitcoinLib.BRL.Util_BRL.getBRLtoBtc(val);
            output.WriteLine(T.ToString());
            Assert.IsType(typeof(double), T);
        }
    }



    public class UnitTest_Receive
    {
        private readonly ITestOutputHelper output;

        public UnitTest_Receive(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Check_ReceivePayment()
        {
            string xAddress = "1NF5LNSbeWHzvaeGbnyPNsJtWeEZm62wix";
            try
            {
                var wr = BitcoinLib.BlockchainAPI.Receive.ReceiveFunds(xAddress, "");
                output.WriteLine($"Input: {wr.InputAddress} \r\n Destination: {wr.DestinationAddress} \r\n Fee: {wr.FeePercent} \r\n Call back URL: {wr.CallbackUrl}");

                Assert.True(wr != null);
            }
            catch (Exception e)
            {
                output.WriteLine($"ERROR: {e.Message}");
                Assert.True(false, e.Message);
            }
        }
    }



    public class UnitTest_BlockExplorer
    {
        private readonly ITestOutputHelper output;

        public UnitTest_BlockExplorer(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("97472e01b6c517382f83b0f3e26b4117bd02b7f009c406fc63d6af1a03e38bc0")]
        public void Check_Tx_Confirmation(string hash)
        {
            try
            {
                var Conf = BitcoinLib.Bitcoin.TxConfirmation(hash);

                output.WriteLine($"Confirmations: {Conf.Confirmation} \r\nDouble Spend: {Conf.DoubleSpend}");

                Assert.True(Conf != null && Conf.Confirmation > 0);
            }
            catch (Exception e)
            {
                output.WriteLine($"ERROR: {e.Message}");
                Assert.True(false, e.Message);
            }
        }
    }
}
