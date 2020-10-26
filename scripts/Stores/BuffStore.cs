using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class BuffStore
{
    public static Dictionary<int, Buff> BuffStoreDic;

    static BuffStore()
    {
        BuffStoreDic = new Dictionary<int, Buff>();
    }
}

