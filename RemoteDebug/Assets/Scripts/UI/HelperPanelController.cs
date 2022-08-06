using etp.xr.Tools;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class HelperPanelController : MonoBehaviour
{
    [SerializeField] private Text m_helperContent;

    [SerializeField] private Button m_closeBtn;

    void Start()
    {
        m_closeBtn.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });

        string name = Dns.GetHostName();
        IPAddress[] ipAdrList = Dns.GetHostAddresses(name);

        var helper = File.ReadAllText(Application.streamingAssetsPath + "/Helper/Helper.txt");

        m_helperContent.text = string.Format(helper, 
            name, 
            ipAdrList[0], 
            ipAdrList[1], 
            DebugDefine.Instance.UTP_PORT, 
            ipAdrList[1], 
            ipAdrList[1],
            DebugDefine.Instance.UTP_PORT,
            DebugDefine.Instance.UTP_PORT,
            DebugDefine.Instance.UTP_PORT,
            DebugDefine.Instance.UTP_PORT);


        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        /*

                设备名称:                     {0}
                本地连接IPv6地址:       {1}
                本地连接IPv4地址:       {2}
                当前监听端口号为:       {3}

                请在代码中设置 IP 地址为 -> {4}
                (ExceptionCatchManager.Instance.UDPIP = "{5}";)

                请在代码中设置端口地址为 -> {6};
                (ExceptionCatchManager.Instance.UDPPort = {7};)

                监听端口号可以在上方输入框中自定义（默认监听9621）
                启动服务器失败请确认本地端口是否被占用.
                 - 启动 CMD.
                 - 运行命令 netstat -aon|findstr \"{8}\" 查询端口占用进程.
                 - 运行命令 tasklist|findstr \"{9}\" 强制关闭进程.
                 - START
         */
    }
}
