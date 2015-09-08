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
        public void getBtcBRLTicker()
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
        public void getBRLtoBtc(double val)
        {
            var T = BitcoinLib.BRL.Util_BRL.getBRLtoBtc(val);
            output.WriteLine(T.ToString());
            Assert.IsType(typeof(double), T);
        }
    }
}
