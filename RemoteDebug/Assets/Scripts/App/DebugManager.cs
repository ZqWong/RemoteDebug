using etp.xr.Managers;
using etp.xr.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class DebugManager : SingletonMonoBehaviourClass<DebugManager>
{
    public const int ITEM_MAX_COUNT = 999;

    public List<Transform> LogItems = new List<Transform>();
    public List<Transform> WarningItems = new List<Transform>();
    public List<Transform> ErrorItems = new List<Transform>();


    private bool m_isLogEnable = true;
    public bool IsLogEnable
    {
        get { return m_isLogEnable; }
        set { m_isLogEnable = value; }
    }

    private bool m_isWarningEnable = true;
    public bool IsWarningEnable
    {
        get { return m_isWarningEnable; }
        set { m_isWarningEnable = value; }
    }

    private bool m_isErrorEnable = true;
    public bool IsErrorEnable
    {
        get { return m_isErrorEnable; }
        set { m_isErrorEnable = value; }
    }

    private void OnEnable()
    {
        SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.LOG_LEVEL_ADJUSTMENT_KEY, LogLevelAdjustment);
        SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.LOG_CLEAN_UP_KEY, LogCleanUp);
    }

    private void OnDisable()
    {
        SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.LOG_LEVEL_ADJUSTMENT_KEY, LogLevelAdjustment);
        SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.LOG_CLEAN_UP_KEY, LogCleanUp);
    }

    private void LogCleanUp(object sender, EventArgs e)
    {
        LogItems.ForEach(i =>
        {
            Destroy(i.gameObject);
        });
        WarningItems.ForEach(i =>
        {
            Destroy(i.gameObject);
        });
        ErrorItems.ForEach(i =>
        {
            Destroy(i.gameObject);
        });
        LogItems.Clear();
        WarningItems.Clear();
        ErrorItems.Clear();
    }

    private void LogLevelAdjustment(object sender, EventArgs e)
    {
        if (e is LogLevelEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case "Log":
                    IsLogEnable = eventArgs.Enable;
                    break;
                case "Warning":
                    IsWarningEnable = eventArgs.Enable;
                    break;
                case "Error":
                    IsErrorEnable = eventArgs.Enable;
                    break;
                default:
                    break;
            }
        }
    }
}

