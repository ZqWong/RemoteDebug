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
using xr.Event;

namespace xr.UI
{
    public class MiddlePanelController : MonoBehaviour
    {
        [SerializeField] private RectTransform m_contentToggleParent;
        [SerializeField] private Toggle m_contentTogglePrefab;
        [SerializeField] private ToggleGroup m_toggleGroup;

        public int LogItemCount = 0;

        private MiddleContentToggleItem m_currentSelectedToggleItem;

        private void OnEnable()
        {
            SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.ADD_DEBUG_DATA_KEY, AddDebugData);
            SingletonProvider<EventManager>.Instance.RegisterEvent(EventKey.LOG_LEVEL_ADJUSTMENT_KEY, LogLevelAdjustment);            
        }

        private void OnDisable()
        {
            SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.ADD_DEBUG_DATA_KEY, AddDebugData);
            SingletonProvider<EventManager>.Instance.UnRegisterEventHandler(EventKey.LOG_LEVEL_ADJUSTMENT_KEY, LogLevelAdjustment);         
        }

        private void LogLevelAdjustment(object sender, EventArgs e)
        {
            if (e is LogLevelEventArgs eventArgs)
            {
                switch (eventArgs.Type)
                {
                    case "Log":
                        DebugManager.Instance.LogItems.ForEach(i => i.gameObject.SetActive(eventArgs.Enable));
                        break;
                    case "Warning":
                        DebugManager.Instance.WarningItems.ForEach(i => i.gameObject.SetActive(eventArgs.Enable));
                        break;
                    case "Error":
                        DebugManager.Instance.ErrorItems.ForEach(i => i.gameObject.SetActive(eventArgs.Enable));
                        break;
                    default:
                        break;
                }
            }
        }

        private void AddDebugData(object sender, EventArgs e)
        {         
            if (e is ExceptionEventArgs eventArgs)
            {
                LogItemCount++;
                Toggle toggle = GameObject.Instantiate(m_contentTogglePrefab);
                toggle.transform.SetParent(m_contentToggleParent);
                MiddleContentToggleItem item = toggle.GetComponent<MiddleContentToggleItem>();
                toggle.group = m_toggleGroup;
                item.Initialize(eventArgs, LogItemCount);

                switch (eventArgs.ExceptionType)
                {
                    case LogType.Log:
                        DebugManager.Instance.LogItems.Add(toggle.transform);
                        CheckMaxRange(DebugManager.Instance.LogItems);
                        break;
                    case LogType.Warning:
                        DebugManager.Instance.WarningItems.Add(toggle.transform);
                        CheckMaxRange(DebugManager.Instance.WarningItems);
                        break;
                    case LogType.Assert:
                    case LogType.Exception:
                    case LogType.Error:
                        DebugManager.Instance.ErrorItems.Add(toggle.transform);
                        CheckMaxRange(DebugManager.Instance.ErrorItems);
                        break;
                    default:
                        break;
                }
            }
        }

        private void CheckMaxRange(List<Transform> target)
        {
            if (target.Count > DebugManager.ITEM_MAX_COUNT)
            {
                var index = target.Count - DebugManager.ITEM_MAX_COUNT;
                Destroy(target[index].gameObject);
                target.RemoveAt(target.Count - DebugManager.ITEM_MAX_COUNT);
            }
        }
    }
}
