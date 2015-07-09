# BitcoinLib
Library that implements the functionality Blockchain API and exchanges APIs.

Until now are implemented the function below.

```
BitcoinLib.BlockchainAPI.ExchangeRates
```
Function  |  Description
----------|--------------
getTicker | Return list of all tickers.
getBtcTicker | Get the value of Blockchain the ticker.
getToBtc  | Returns the value converted into Bitcoin.
getCurrencyCodeToBtc  | Returns the value in the country's currency.

<br />
```
BitcoinLib.BlockchainAPI.Wallet
```
Function  |  Description
----------|--------------
Wallet | Your wallet information.
getWalletAddress | Return all wallet addresses.

<br />
```
Bitcoin.BRL.Util_BRL
```
Function  |  Description
----------|--------------
getBtcBRLTicker | Pega o ticker da Blockchain com o valor em R$ BRL.
getBRLtoBtc | Retorna o valor em R$ BRL convertido para Bitcoin (btc).
