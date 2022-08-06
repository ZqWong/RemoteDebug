using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using etp.xr.Tools;
using etp.xr.Managers;
using xr.Event;
using etp.xr.exceptioncatch;

/// <summary>UDP服务器</summary>
public class UDPServer : SingletonMonoBehaviour<UDPServer>
{
    #region 数据申明

    private byte[] m_data;
    private Socket m_socket;
    private int m_recv;
    private Thread m_thread;
    private EndPoint m_endPoint;
    private Queue m_msgQueue;

    #endregion
    
    private void Awake()
    {       
        m_msgQueue = new Queue();      
    }

    private void OnEnable()
    {
        SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.START_SERVICE_KEY, StartService);
    }

    private void OnDisable()
    {
        SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.START_SERVICE_KEY, StartService);
    }

    private void StartService(object sender, EventArgs e)
    {
        if (m_thread != null && m_thread.IsAlive)
        {
            m_thread.Abort();
        }        
        InitServer();
    }

    private void OnDestroy()
    {
        Clear();
    }

    private void Update()
    {
        if (m_msgQueue.Count != 0)
        {
            var item = m_msgQueue.Dequeue();            
            var msg = LitJson.JsonMapper.ToObject<ExceptionEventArgs>(item.ToString());            
            SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.ADD_DEBUG_DATA_KEY, msg);
        }
    }

    #region 公开函数

    public void InitServer()
    {
        try
        {
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, DebugDefine.Instance.UTP_PORT);
            m_socket.Bind(iep);
            m_endPoint = (EndPoint)iep;

            m_thread = new Thread(Receive);
            m_thread.Start();
            Debug.Log("启动UDP服务成功");

            ExceptionEventArgs log = new ExceptionEventArgs(DateTime.Now.ToString(), LogType.Warning, "启动UDP服务成功", $"-端口号 : {DebugDefine.Instance.UTP_PORT}");
            SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.ADD_DEBUG_DATA_KEY, log);
        }

        catch (Exception e)
        {
            ExceptionEventArgs log = new ExceptionEventArgs(DateTime.Now.ToString(), LogType.Error, "准备启动UDP失败:" + e.Message, e.StackTrace);
            SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.ADD_DEBUG_DATA_KEY, log);
        }
    }

    #endregion


    #region 私有函数

    private void Receive()
    {
        string msg = string.Empty;
        while (true)
        {
            m_data = new byte[1024 * 1024];
            m_recv = m_socket.ReceiveFrom(m_data, ref m_endPoint);
            msg = Encoding.UTF8.GetString(m_data, 0, m_recv);

           Debug.Log(msg);
            m_msgQueue.Enqueue(msg);
        }
    }

    private void Clear()
    {
        if (m_socket != null)
        {
            m_socket.Close();
            m_socket = null;
        }
        m_thread?.Abort();
    }

    #endregion

}
