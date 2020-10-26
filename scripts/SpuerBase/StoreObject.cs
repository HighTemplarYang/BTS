using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class StoreObject:Ownerable,ICopyable
{
    public abstract ICopyable Copy();

    public StoreObject Obtain(Unit Self)
    {
        StoreObject temp = (StoreObject)Copy();
        temp.Init(Self);
        return temp;
    }

}

