using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class StartServerExample 
{
    private static string uniqueId = System.Guid.NewGuid().ToString();

    Coroutine server;
    public string message;
    
    void Start()
    {
        Action<string> printAction = i => {
            this.message = message;
            StopCoroutine(server);
        };

        this.server = StartCoroutine(SocketServer.StartServer(uniqueId, printAction));
    }
}
