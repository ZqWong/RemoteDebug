using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using etp.xr.Tools;

public class DebugDefine : SingletonMonoBehaviourClass<DebugDefine>
{
    /// <summary>UTP�˿ں�</summary>
    public int UTP_PORT = 9621;

    /// <summary>�Ƿ��ӡ�ռ�</summary>
    public static bool IsPrintLog
    {
        get
        {
#if PRINT_LOG
                bool printLog = true;
#else
            bool printLog = false;
#endif
            return printLog;
        }
    }

    /// <summary>�Ƿ��ǵ���ģʽ</summary>
    public static bool IsDebugMode
    {
        get
        {
#if DEBUG_MODE
                bool debugMode = true;
#else
            bool debugMode = false;
#endif
            return debugMode;
        }
    }
}

