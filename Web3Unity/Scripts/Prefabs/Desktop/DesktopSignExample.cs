using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopSignExample : MonoBehaviour
{
    public Text btn;
    async public void OnSign()
    {
        string message = "hello";
        string signature = await Web3Desktop.Sign(message);
        // either "0xSignature" or "error"
        Debug.Log(signature);
        btn.text = signature;
    }
}
