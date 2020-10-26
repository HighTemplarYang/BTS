using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SkillBase:Ownerable
{
    public int ID;
    public string Name;
    public int BaseCoolDown;
    public int CurrentCoolDown;
    public BattleActionGroup SkillActions;
    public Cost cost;

    public SkillBase(int ID, string Name, int BaseCoolDown, BattleActionGroup SkillActions, Cost cost)
    {
        this.ID = ID;
        this.Name = Name;
        this.BaseCoolDown = BaseCoolDown;
        this.SkillActions = SkillActions;
        this.cost = cost;
        CurrentCoolDown = 0;
    }

    public bool CanCast()
    {
        return CheckCost() && CurrentCoolDown == 0;
    }

    public bool TryCast()
    {
        if (!CanCast())
        {
            return false;
        }
        else
        {
            Self.BattleResourcesGroup.PayResourceGroup(cost);
            Settlement();
            return true;
        }
    }

    public bool CheckCost()
    {
        return Self.BattleResourcesGroup.ResourceGroupCheck(cost);
    }

    public void Settlement()
    {
        SkillActions.Act();
        CurrentCoolDown = BaseCoolDown;
    }

    public void CoolDownReduce(int num)
    {
        CurrentCoolDown -= num;
        CurrentCoolDown = Math.Max(0, CurrentCoolDown);
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        cost.Init(Self);
        SkillActions.Init(Self);
    }


}
