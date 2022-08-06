using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EventKey
{
    /// <summary>
    /// 启动服务
    /// </summary>
    public const string START_SERVICE_KEY = "START_SERVICE";
    /// <summary>
    /// 更新最后一个log的堆栈
    /// </summary>
    public const string OPEN_LAST_INFO_KEY = "OPEN_LAST_INFO";
    /// <summary>
    /// 添加log信息
    /// </summary>
    public const string ADD_DEBUG_DATA_KEY = "ADD_DEBUG_DATA";
    /// <summary>
    /// 更新log数量
    /// </summary>
    public const string UPDATE_DEBUG_COUNT_KEY = "UPDATE_DEBUG_COUNT";
    /// <summary>
    /// 显示堆栈
    /// </summary>
    public const string SHOW_STACKTRACE_KEY = "SHOW_STACKTRACE";
    /// <summary>
    /// 变更日志输出级别
    /// </summary>
    public const string LOG_LEVEL_ADJUSTMENT_KEY = "LOG_LEVEL_ADJUSTMENT";
    /// <summary>
    /// 清空LOG
    /// </summary>
    public const string LOG_CLEAN_UP_KEY = "LOG_CLEAR_UP";
}

