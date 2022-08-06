using etp.xr.exceptioncatch;
using etp.xr.Managers;
using etp.xr.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace xr.UI
{
    public class TopPanelController : MonoBehaviour
    {
        [SerializeField] private Button m_buttonQuit;
        [SerializeField] private Button m_buttonClear;
        [SerializeField] private Button m_buttonStart;
        [SerializeField] private Toggle m_toggleCollapse;
        [SerializeField] private Toggle m_toggleLog;
        [SerializeField] private Toggle m_toggleWarning;
        [SerializeField] private Toggle m_toggleError;
        [SerializeField] private InputField m_inputFieldUDPPort;

        private void OnEnable()
        {
            SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.ADD_DEBUG_DATA_KEY, AddDebugData);
            //SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.SHOW_STACKTRACE_KEY, UpdateSelectedItem);
            SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.LOG_CLEAN_UP_KEY, LogCleanUp);
        }

        private void OnDisable()
        {
            SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.ADD_DEBUG_DATA_KEY, AddDebugData);
            //SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.SHOW_STACKTRACE_KEY, UpdateSelectedItem);
            SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.LOG_CLEAN_UP_KEY, LogCleanUp);
        }

        private void LogCleanUp(object sender, EventArgs e)
        {
            m_toggleLog.GetComponentInChildren<Text>().text = "0";
            m_toggleWarning.GetComponentInChildren<Text>().text = "0";
            m_toggleError.GetComponentInChildren<Text>().text = "0";
        }

        private void AddDebugData(object sender, EventArgs e)
        {
            UpdateToggleItemCount();
        }

        private void UpdateToggleItemCount()
        {
            m_toggleLog.GetComponentInChildren<Text>().text = DebugManager.Instance.LogItems.Count.ToString();
            m_toggleWarning.GetComponentInChildren<Text>().text = DebugManager.Instance.WarningItems.Count.ToString();
            m_toggleError.GetComponentInChildren<Text>().text = DebugManager.Instance.ErrorItems.Count.ToString();
        }

        private void Start()
        {
            m_toggleLog.onValueChanged.AddListener((isOn) =>
            {
                SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.LOG_LEVEL_ADJUSTMENT_KEY, new LogLevelEventArgs("Log", isOn));
            });

            m_toggleWarning.onValueChanged.AddListener((isOn) =>
            {
                SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.LOG_LEVEL_ADJUSTMENT_KEY, new LogLevelEventArgs("Warning", isOn));
            });

            m_toggleError.onValueChanged.AddListener((isOn) =>
            {
                SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.LOG_LEVEL_ADJUSTMENT_KEY, new LogLevelEventArgs("Error", isOn));
            });

            m_inputFieldUDPPort.onEndEdit.AddListener((value) =>
            {
                int port = DebugDefine.Instance.UTP_PORT;
                try
                {
                    port = int.Parse(m_inputFieldUDPPort.text);
                }
                catch (Exception e)
                {
                    ExceptionEventArgs log = new ExceptionEventArgs(DateTime.Now.ToString(), LogType.Warning, "端口号解析失败", e.Message);
                    SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.ADD_DEBUG_DATA_KEY, log);
                }
                m_inputFieldUDPPort.placeholder.GetComponent<Text>().text = port.ToString();
                DebugDefine.Instance.UTP_PORT = port;
            });

            m_buttonQuit.onClick.AddListener(() =>
            {
                Application.Quit();
            });

            m_buttonClear.onClick.AddListener(() =>
            {
                SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.LOG_CLEAN_UP_KEY, null);
            });

            m_buttonStart.onClick.AddListener(() =>
            {
                SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.START_SERVICE_KEY, null);
            });
        }
    }
}
