using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mx.Log;
using etp.xr.Tools;
using etp.xr.exceptioncatch;
using etp.xr.Managers;


namespace Mx.Example
{
    /// <summary>测试远程调试功能</summary>
    public class TestRemoteDebug : MonoBehaviour
    {
        void Start()
        {

        }

        private void Awake()
        {
            //是否启动消息捕获
            ExceptionCatchManager.Instance.EnableHandler = true;
            //远程调试端IP
            ExceptionCatchManager.Instance.UDPIP = "172.16.7.128";
            //远程调试端接口
            ExceptionCatchManager.Instance.UDPPort = 9621;
            //是否启动远程调试
            ExceptionCatchManager.Instance.EnableRemoteDebug = true;
            //本地Log信息存储位置
            ExceptionCatchManager.Instance.LogLocalPath = Application.persistentDataPath;
            //是否启动本地Log信息存储
            ExceptionCatchManager.Instance.EnableRecords = true;
        }


        /// <summary>
        /// 绑定消息捕获时间回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExceptionEventHandler(object sender, System.EventArgs e)
        {
            // Todo:...
        }

        private void OnEnable()
        {
            SingletonProvider<EventManager>.Instance.RegisterEvent(ExceptionDefine.EXCEPTION_CATCHED_EVENT_KEY, ExceptionEventHandler);
        }

        private void OnDisable()
        {
            SingletonProvider<EventManager>.Instance.RegisterEvent(ExceptionDefine.EXCEPTION_CATCHED_EVENT_KEY, ExceptionEventHandler);
        }

        public void OnPrintLog()
        {
            int index1 = Random.Range(0, 3);
            int index2 = Random.Range(0, 10);

            if (index1 == 0)
            {
                Debug.Log(GetType() + "/PrintLog()/" + "请前往RemoteDebug工具查看Logo" + index2);
            }
            else if (index1 == 1)
            {
                Debug.LogWarning(GetType() + "/PrintLog()/" + "请前往RemoteDebug工具查看Logo" + index2);
            }
            else
            {
                Debug.LogError(GetType() + "/PrintLog()/" + "请前往RemoteDebug工具查看Logo" + index2);
            }
        }
    }
}