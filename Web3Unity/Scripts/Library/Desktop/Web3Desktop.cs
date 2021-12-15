using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using UnityEngine;

public class Web3Desktop
{
    private static readonly string wsUri = "ws://localhost:3001/app";
    private static readonly string walletHost = "http://localhost:3000";
    private static string response = "";
    private static ClientWebSocket ws;

    async public static Task<string> SendTransaction(string _network, string _to, string _value, string _data = "", string _gasLimit = "", string _gasPrice = "")
    {
        ws = new ClientWebSocket();
        await ws.ConnectAsync(new Uri(wsUri), CancellationToken.None);
        OpenWS();
        // generate uuid
        Guid guid = Guid.NewGuid();
        // remove "-" from uuid
        string id = String.Join("", guid.ToString().Split('-'));
        // send socket message with id
        await ws.SendAsync(new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(id)), WebSocketMessageType.Text, true, CancellationToken.None);
        // open wallet
        Application.OpenURL(walletHost + "?action=send&network=" + _network + "&id=" + id + "&to=" + _to + "&value=" + _value + "&gasLimit=" + _gasLimit + "&gasPrice=" + _gasPrice + "&data=" + _data);
        // wait for response
        while (response == "") await Task.Delay(1000);
        // set signature
        string signature = response;
        // close socket
        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        // reset response
        response = "";
        return signature;
    }

    async public static Task<string> Sign(string _message)
    {
        ws = new ClientWebSocket();
        await ws.ConnectAsync(new Uri(wsUri), CancellationToken.None);
        OpenWS();
        // generate uuid
        Guid guid = Guid.NewGuid();
        // remove "-" from uuid
        string id = String.Join("", guid.ToString().Split('-'));
        // send socket message with id
        await ws.SendAsync(new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(id)), WebSocketMessageType.Text, true, CancellationToken.None);
        // open wallet
        Application.OpenURL(walletHost + "?action=sign&id=" + id + "&message=" + _message);
        // wait for response
        while (response == "") await Task.Delay(1000);
        // set signature
        string signature = response;
        // close socket
        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        // reset response
        response = "";
        return signature;
    }

    async private static void OpenWS()
    {
        // receive response
        ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[8192]);
        while (ws.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = await ws.ReceiveAsync(buffer, CancellationToken.None);
            response = System.Text.Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
        };
    }
}
