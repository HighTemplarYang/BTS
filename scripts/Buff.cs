using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BuffMachine
{
    List<Buff> buffList;
    Unit Self;

    public BuffMachine(Unit Self)
    {
        this.Self = Self;
        buffList = new List<Buff>();
    }

    public void SetBuff(Buff buff)
    {

        if(HaveOverlayBuff(buff,out Buff tempbuff))
        {
            tempbuff.BuffOverLay(buff.LayNum);
        }
        else
        {
            buffList.Add(buff);
        }
        buff.Self = this.Self;
        buff.OnBuffSet();
    }

    bool HaveOverlayBuff(Buff BuffCheck,out Buff buff)
    {
        if (!BuffCheck.Overlay)
        {
            buff = null;
            return false;
        } 
        foreach(var bf in buffList)
        {
            if (bf.ID == BuffCheck.ID)
            {
                buff = bf;
                return true;
            }
        }
        buff = null;
        return false;
    }

    public void EndBuff(Buff buff)
    {
        buffList.Remove(buff);
        buff.OnBuffEnd();
    }

    public void TurnEnd()
    {
        List<Buff> WaitToRemove = new List<Buff>();
        foreach(var bf in buffList)
        {
            bf.OnBuffTurnEnd();
            if (bf.RemainTurnNum <= 0)
            {
                WaitToRemove.Add(bf);
            }
        }
        foreach(Buff bf in WaitToRemove)
        {
            EndBuff(bf);
        }
    }

    

}

public enum BuffType
{
    Magic,
    Curse,
    Disease,
    Poisoning
} 

public class Buff
{
    public int ID;
    public BuffType buffType;
    public int turnNumber;
    public int RemainTurnNum;
    public List<Fix> fixes;
    public bool Overlay;
    public bool OverlayRefreshTurn;
    public int LayNum;
    public Unit Self;

    public BattleActionGroup battleActionGroupOnSet;
    public BattleActionGroup battleActionGroupOnEnd;
    public BattleActionGroup battleActionGroupOnTurnEnd;

    public Buff(int ID,BuffType buffType,int turnNumber, List<Fix> fixes,bool Overlay=false,bool OverlayRefreshTurn=true, int LayNum=1, BattleActionGroup battleActionGroupOnSet=null, BattleActionGroup battleActionGroupOnEnd=null, BattleActionGroup battleActionGroupOnTurnEnd = null)
    {
        this.ID = ID;
        this.buffType = buffType;
        this.turnNumber = turnNumber;
        this.RemainTurnNum = turnNumber;
        this.fixes = fixes;
        this.battleActionGroupOnEnd = battleActionGroupOnEnd;
        this.battleActionGroupOnSet = battleActionGroupOnSet;
        this.battleActionGroupOnTurnEnd = battleActionGroupOnTurnEnd;
        this.Overlay = Overlay;
        this.LayNum = LayNum;
        this.OverlayRefreshTurn = OverlayRefreshTurn;   
    }

    public static Buff Copybuff(Buff buff)
    {
        if (buff == null)
        {
            return null;
        }
        return new Buff(buff.ID, buff.buffType, buff.turnNumber, buff.fixes, buff.Overlay, buff.OverlayRefreshTurn, buff.LayNum, buff.battleActionGroupOnSet, buff.battleActionGroupOnEnd, buff.battleActionGroupOnTurnEnd);
    }

    public void BuffOverLay(int LayNum)
    {
        this.LayNum += LayNum;
        if (OverlayRefreshTurn)
        {
            RemainTurnNum = turnNumber;
        }
    }

    public void OnBuffSet()
    {
        if(battleActionGroupOnSet!=null)
            battleActionGroupOnSet.Act();
    }

    public void OnBuffEnd()
    {
        if(battleActionGroupOnEnd!=null)
            battleActionGroupOnEnd.Act();
    }

    public void OnBuffTurnEnd()
    {
        if (battleActionGroupOnTurnEnd != null)
            battleActionGroupOnTurnEnd.Act();
        RemainTurnNum--;
    }

   
}


public class Fix
{
    public int PropertyID;

    public float AddCorrectRate;
    public float AdditionCorrectStatic;
    public float MultiCorrectRate;
    public float BaseCorrectStatic;

    public Fix(int PropertyID, float AddCorrectRate, float BaseCorrectStatic, float MultiCorrectRate, float AdditionCorrectStatic)
    {
        this.PropertyID = PropertyID;
        this.AddCorrectRate = AddCorrectRate;
        this.AdditionCorrectStatic = AdditionCorrectStatic;
        this.MultiCorrectRate = MultiCorrectRate;
        this.BaseCorrectStatic = BaseCorrectStatic;
    }

    public Fix(int PropertyID)
    {
        this.PropertyID = PropertyID;
        this.AddCorrectRate = 0;
        this.AdditionCorrectStatic = 0;
        this.MultiCorrectRate = 1;
        this.BaseCorrectStatic = 0;
    }

    public static Fix operator+(Fix fix1,Fix fix2)
    {
        if (fix1.PropertyID != fix2.PropertyID)
        {
            return null;
        }
        else
        {
            return new Fix(fix1.PropertyID, fix1.AddCorrectRate + fix2.AddCorrectRate, fix1.BaseCorrectStatic + fix2.BaseCorrectStatic, fix1.MultiCorrectRate * fix2.MultiCorrectRate, fix1.AddCorrectRate + fix2.AddCorrectRate);
        }
    }

}


public class FixGroup
{
    Dictionary<int, List<Fix>> FixDic;

    public FixGroup()
    {
        FixDic = new Dictionary<int, List<Fix>>();
    }

    public void AddFix(Fix fix)
    {
        if (!FixDic.ContainsKey(fix.PropertyID))
        {
            FixDic.Add(fix.PropertyID, new List<Fix>());
        }
        FixDic[fix.PropertyID].Add(fix);
    }

    public void RemoveFix(Fix fix)
    {
        FixDic[fix.PropertyID].Remove(fix);
    }

    public Fix GetFix(int id)
    {
        Fix TempFix = new Fix(id);
        if (FixDic.ContainsKey(id))
        {
            foreach(var fix in FixDic[id])
            {
                TempFix += fix;
            }
        }
        return TempFix;
    }

}



