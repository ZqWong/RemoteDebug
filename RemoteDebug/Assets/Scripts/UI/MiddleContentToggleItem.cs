using etp.xr.exceptioncatch;
using etp.xr.Managers;
using etp.xr.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiddleContentToggleItem : MonoBehaviour
{
    [SerializeField] private Sprite m_logSprite;
    [SerializeField] private Sprite m_warningSprite;
    [SerializeField] private Sprite m_errorSprite;
    
    [SerializeField] private Text m_messateText;

    [SerializeField] private Image m_messageBackgroundImage;
    [SerializeField] private Image m_messageTypeImage;
    
    public ExceptionEventArgs CurrentItemContent;

    private Toggle m_toggleComponent;
    private Color32 m_currentBgColor;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(m_logSprite != null, "异常未找到对象 m_logSprite");
        Debug.Assert(m_warningSprite != null, "异常未找到对象 m_warningSprite");
        Debug.Assert(m_errorSprite != null, "异常未找到对象 m_errorSprite");
        Debug.Assert(m_messageTypeImage != null, "异常未找到对象 m_messageTypeImage");
        Debug.Assert(m_messageBackgroundImage != null, "异常未找到对象 m_messageBackgroundImage");
        Debug.Assert(m_messateText != null, "异常未找到对象 m_messateText");

        m_toggleComponent = GetComponent<Toggle>();
        m_toggleComponent.onValueChanged.AddListener((isOn) => 
        {
            if (isOn)
            {
                SingletonProvider<EventManager>.Instance.RaiseEventByEventKey(EventKey.SHOW_STACKTRACE_KEY, CurrentItemContent);
                //m_messageBackgroundImage.color = Palette.Instance.ToggleHighlight;
            }
            else
            {
                //m_messageBackgroundImage.color = m_currentBgColor;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(ExceptionEventArgs eventArgs, int index)
    {
        //m_currentBgColor = index % 2 == 1 ? Palette.Instance.ToggleDefault_light : Palette.Instance.ToggleDefault_dark;
        //m_messageBackgroundImage.color = m_currentBgColor;        

        CurrentItemContent = eventArgs;
        m_messateText.text = $"{CurrentItemContent.ThrowTime} {CurrentItemContent.ExceptionMessage}";
        switch (eventArgs.ExceptionType)
        {
            case LogType.Log:
                gameObject.SetActive(DebugManager.Instance.IsLogEnable);
                m_messateText.color = new Color(236, 236, 236, 236);
                m_messageTypeImage.sprite = m_logSprite;
                m_messageTypeImage.color = new Color(236, 236, 236, 236);
                break;
            case LogType.Warning:
                gameObject.SetActive(DebugManager.Instance.IsWarningEnable);
                m_messateText.color = new Color(201, 151, 0, 255);
                m_messageTypeImage.sprite = m_warningSprite;
                m_messageTypeImage.color = new Color(201, 151, 0, 255);
                //m_messageBackgroundImage.color = Palette.Instance.WarningDefalut;
                break;
            case LogType.Assert:
            case LogType.Exception:
            case LogType.Error:
                gameObject.SetActive(DebugManager.Instance.IsErrorEnable);
                //m_messateText.color = new Color(134, 28, 28, 255);
                m_messateText.color = Color.red;
                m_messageTypeImage.sprite = m_errorSprite;
                m_messageTypeImage.color = Color.red;
                break;
            default:
                break;
        }
    }
}
