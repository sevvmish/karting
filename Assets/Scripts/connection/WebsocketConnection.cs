using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using TMPro;
using KartGame.KartSystems;
using System.Drawing;
using System;

public class WebsocketConnection : MonoBehaviour
{

    private WebSocket websocket;
    private float _timer;
    private ArcadeKart playerKart;
    private int order;
    private PlayersUpdate players;
    private InputController inputController;
    private bool isInitedStart;
    private float _initer;

    public void StartConnection(ArcadeKart kart, TextMeshProUGUI dataText, PlayersUpdate pl, InputController input)
    {        
        inputController = input;
        playerKart = kart;
        players = pl;
        websocket = new WebSocket($"ws://{Globals.IP}:{Globals.Port}");


        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {            
            byte first = bytes[0];



            if (first == 1 && Globals.Approved)
            {
                byte[] result = new byte[bytes.Length-1];
                Array.Copy(bytes, 1, result, 0, (bytes.Length - 1));
                StandartPacket p = StandartPacket.GetPacket(result);
                pl.PacketHandler(p);
            }

            if (first == 0)
            {
                byte[] result = new byte[bytes.Length - 1];
                Array.Copy(bytes, 1, result, 0, (bytes.Length - 1));
                StartPacket p = StartPacket.GetPacket(result);
                pl.AddMainPlayerPosition(new Vector3(p.PositionX, p.PositionY, p.PositionZ));
            }
        };

     
        // START
        websocket.Connect();
        

    }

    void Update()
    {
        if (_initer > 0.1f && !isInitedStart)
        {
            isInitedStart = true;
            StartPacket s = new StartPacket(Globals.NetworkID, Globals.SecretID, 0, 0, 0);
            websocket.Send(StartPacket.GetBytes(s));
        }
        else
        {
            _initer += Time.deltaTime;
        }


        if (websocket.State == WebSocketState.Open && Globals.Approved)
        {
            //if (_timer > Globals.ServerTickFloat)
            //{
                _timer = 0;
                StandartPacket p = inputController.GetMainPacket();
            
                websocket.Send(StandartPacket.GetBytes(p));
            //}
            //else
            //{
                _timer += Time.deltaTime;
            //}
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
