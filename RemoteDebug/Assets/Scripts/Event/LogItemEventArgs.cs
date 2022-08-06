using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LogItemEventArgs : EventArgs
{

    public MiddleContentToggleItem MiddleContentToggleItem;

    public LogItemEventArgs(MiddleContentToggleItem middleContentToggleItem)
    {
        MiddleContentToggleItem = middleContentToggleItem;
    }
}

