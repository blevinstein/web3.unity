using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopSendTransactionExample : MonoBehaviour
{
    public Text btn;
    async public void OnSendTransaction()
    {
        /*
        1 Mainnet
        4 Rinkeby
        56 Binance Smart Chain Mainnet
        97 Binance Smart Chain Testnet
        137 Matic
        80001 Matic Testnet
        */
        string network = "4";
        // account to send to
        string to = "0xdD4c825203f97984e7867F11eeCc813A036089D1";
        // amount in wei to send
        string value = "1230000000000000";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";

        string response = await Web3Desktop.SendTransaction(network, to, value, gasLimit, gasPrice);
        // either "0xSignature" or "error"
        Debug.Log(response);
        btn.text = response;
    }
}
