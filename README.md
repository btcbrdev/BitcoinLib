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
Send | Send bitcoin from your wallet to another bitcoin address.
getWalletAddress | List all active addresses in a wallet.
getAddressBallance | etrieve the balance of a bitcoin address.
WalletBalance | Get balance of all wallet address.
NewAddress | Generating a new address.
ArchiveAddress | Addresses which have not been used recently should be moved to an archived state.
UnArchiveAdrres | Unarchive an address. Will also restore consolidated addresses.
Consolidate | The consolidate command will remove some inactive archived addresses from the wallet and insert them as forwarding addresses.


<br />
```
Bitcoin.BRL.Util_BRL
```
Function  |  Description
----------|--------------
getBtcBRLTicker | Retorna o ticker da Blockchain com o valor em BRL.
getBRLtoBtc | Retorna o valor em R$ BRL convertido para Bitcoin (btc).
