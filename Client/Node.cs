using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

using WebSocketSharp;

public class Node : MonoBehaviour
{
    private WebSocket ws;
    Timer timer;

    void Start()
    {
        StartTimer();
        Ws_Start(1);
    }

    void Ws_Start(int roomNumber)
    {
        ws = new WebSocket("ws://127.0.0.1:8888?room=" + roomNumber);
        ws.OnMessage += ws_OnMessage; 
        ws.OnOpen += ws_OnOpen;
        ws.OnClose += ws_OnClose;

        ws.Connect();
    } 

    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log($"{((WebSocket)sender).Url} received data : {e.Data}");
    }
    void ws_OnOpen(object sender, System.EventArgs e)
    {
        Debug.Log("socket open"); 
    }
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("socket close"); 
        DisconnectServer();
    }

    void StartTimer()
    {
        timer = new Timer();
        timer.Interval = 5000;
        timer.Elapsed += OnTimerEvent;
        timer.AutoReset = true;
        timer.Enabled = true;
        timer.Start();
    }
    void DisconnectServer() 
    {
        if(ws.IsAlive) 
        {
            StopTimer();
            ws.Close();
        } 
    }

    void StopTimer()
    {
        timer.Stop();
    }

    void OnTimerEvent(System.Object source, ElapsedEventArgs e) 
    {
        ws.Send("자동메세지 : 안녕 난 1번이야" + System.DateTime.Now);
    }

    void Update()
    {
        if (ws == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("안녕 난 1번이야");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ws.Send("위로 1");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ws.Send("아래 1");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ws.Send("왼쪽 1");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ws.Send("오른쪽 1");
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            ws.Send("간다 1");
            DisconnectServer();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            if(!ws.IsAlive) {
                StartTimer();
                Ws_Start(1);
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!ws.IsAlive) {
                StartTimer();
                Ws_Start(2);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!ws.IsAlive) {
                StartTimer();
                Ws_Start(3);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.T))
        {
            StopTimer();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            StartTimer();
        }
    }

    private void OnApplicationQuit() {
        DisconnectServer();
    }

}