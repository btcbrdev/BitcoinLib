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
                var wr = BitcoinLib.BlockchainAPI.Receive.ReceivePayments(xAddress, "");
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

        [Fact]
        public void Check_Transaction()
        {
            //string t = "{'tx_hash' : 968dcebdb66f441fcd154878bb1b79b7275848c0f8d55a3cef66bcc06af6fad7}";
            //string json = @"{ gettransaction: '968dcebdb66f441fcd154878bb1b79b7275848c0f8d55a3cef66bcc06af6fad7+',}";

            //var jsHash = Newtonsoft.Json.Linq.JObject.Parse(json);
            //jsHash.Add(Newtonsoft.Json.Linq.JObject.Parse(t));
            //jsHash.Add("tx_hash", Newtonsoft.Json.Linq.JToken.Parse("968dcebdb66f441fcd154878bb1b79b7275848c0f8d55a3cef66bcc06af6fad7"));

            try
            {
                var x = new Info.Blockchain.API.BlockExplorer.BlockExplorer(); // .Transaction(jsHash);
                var t = x.GetBlock("968dcebdb66f441fcd154878bb1b79b7275848c0f8d55a3cef66bcc06af6fad7");

                output.WriteLine(t.Transactions.Count.ToString());

                Assert.True(t != null && t.Transactions.Count > 0);
            }
            catch (Exception e)
            {
                output.WriteLine($"ERROR: {e.Message}");
                Assert.True(false, e.Message);
            }
        }
    }
}
