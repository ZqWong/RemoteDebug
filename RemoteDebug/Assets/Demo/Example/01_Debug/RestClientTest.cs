//using System.Collections;
//using System.Collections.Generic;
//using etp.xr.Managers;
//using UnityEngine;
//using UnityEngine.UI;

//public class ResultModule
//{
//    public int code;
//    public string msg;

//    public int id;
//    public int noticeProjectId;
//    public string content;
//    public string title;
//    public int sortNumber;
//    public string delFlag;
//    public string createTime;
//    public string updateTime;
//    public int isShow;
//}

//public class RestClientTest : MonoBehaviour
//{
//    [SerializeField] private Button m_getNoticeButton; 

//    // Start is called before the first frame update
//    void Start()
//    {
//        m_getNoticeButton.onClick.AddListener(() =>
//        {
//            string rets = new RestClientManager<string>().Get(@"http://localhost:9999/notice/etpappnoticeitem/project/1", new
//            {
//                Authorization = "Bearer 0e3d45f9-a439-445e-946f-c9f535eac9e9",
//            });

//            Debug.Log(rets);
//        });


//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
