using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ownerable
{
    public Unit Self;

    public virtual void Init(Unit Self)
    {
        this.Self = Self;
    }
}

