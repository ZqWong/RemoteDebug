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

                �豸����:                     {0}
                ��������IPv6��ַ:       {1}
                ��������IPv4��ַ:       {2}
                ��ǰ�����˿ں�Ϊ:       {3}

                ���ڴ��������� IP ��ַΪ -> {4}
                (ExceptionCatchManager.Instance.UDPIP = "{5}";)

                ���ڴ��������ö˿ڵ�ַΪ -> {6};
                (ExceptionCatchManager.Instance.UDPPort = {7};)

                �����˿ںſ������Ϸ���������Զ��壨Ĭ�ϼ���9621��
                ����������ʧ����ȷ�ϱ��ض˿��Ƿ�ռ��.
                 - ���� CMD.
                 - �������� netstat -aon|findstr \"{8}\" ��ѯ�˿�ռ�ý���.
                 - �������� tasklist|findstr \"{9}\" ǿ�ƹرս���.
                 - START
         */
    }
}
