using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LogLevelEventArgs : EventArgs
{
    public string Type;
    public bool Enable;

    public LogLevelEventArgs(string type, bool enable)
    {
        Type = type;
        Enable = enable;
    }
}

