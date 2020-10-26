using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionGroup:Ownerable
{
    List<BattleActionBase> battleActionBaseList;

    public BattleActionGroup(List<BattleActionBase> battleActionBaseList)
    {
        this.battleActionBaseList = battleActionBaseList;
    }

    public void Act()
    {
        foreach(var action in battleActionBaseList)
        {
            action.Act();
        }
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        foreach(var act in battleActionBaseList)
        {
            act.Init(Self);
        }
    }

}

public enum BattleActionType
{
    DealDamage,
    Healing,
    SetBuff,
    DisperseBuff
}
public abstract class BattleActionBase:Ownerable 
{
    public BattleActionType battleActionType;
    public TargetType Targets;

    public BattleActionBase(BattleActionType battleActionType, TargetType Targets)
    {
        this.battleActionType = battleActionType;
        this.Targets = Targets;
    }

    public abstract bool Act();
}

public class SetBuffAction : BattleActionBase
{
    public Buff buff;
    public SetBuffAction(Buff buff, TargetType Targets) : base(BattleActionType.SetBuff, Targets)
    {
        this.buff = buff;
    }

    public override bool Act()
    {

        foreach (var target in UnitManager.Instance.GetUnits(Self,Targets))
        {
            target.SetBuff(Buff.Copybuff(buff));
        }
        return true;
    }
}


public class DealDamageAction: BattleActionBase
{
    public OriginDamage originDamage;
    public DealDamageAction(OriginDamage originDamage,TargetType Targets):base(BattleActionType.DealDamage,Targets)
    {
        this.originDamage = originDamage;
    }

    public override bool Act()
    {
        FinnalDamage finnalDamage = Self.CorrectDamage(originDamage);
        foreach(var target in UnitManager.Instance.GetUnits(Self, Targets))
        {
            target.TakeDamage(finnalDamage);
        }
        return true;
    }
}