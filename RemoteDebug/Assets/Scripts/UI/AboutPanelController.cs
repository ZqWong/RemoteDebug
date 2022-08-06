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
    public class AboutPanelController : MonoBehaviour
    {

        [SerializeField] private Text m_lastLogMessageText;

        private void OnEnable()
        {
            SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.ADD_DEBUG_DATA_KEY, ShowLastLog);
            SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.LOG_CLEAN_UP_KEY, LogCleanUp);
        }

        private void OnDisable()
        {
            SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.ADD_DEBUG_DATA_KEY, ShowLastLog);
            SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.LOG_CLEAN_UP_KEY, LogCleanUp);
        }

        private void LogCleanUp(object sender, EventArgs e)
        {
            m_lastLogMessageText.text = "";
        }

        private void ShowLastLog(object sender, EventArgs e)
        {
            if (e is ExceptionEventArgs eventArgs)
            {
                m_lastLogMessageText.text = $"{eventArgs.ThrowTime} {eventArgs.ExceptionMessage}";
            }
        }
    }
}
