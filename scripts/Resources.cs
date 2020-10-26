using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleResources
{
    public int ID;
    public string Name;
    public int ResourcesMax;
    public int CurrentResources;

    public BattleResources(int ID,string Name,int ResourcesMax,int CurrentResources)
    {
        this.ID = ID;
        this.Name = Name;
        this.ResourcesMax = ResourcesMax;
        this.CurrentResources = CurrentResources;
    }

    public bool ResourceCheck(int Cost)
    {
        return Cost <= CurrentResources;
    }

    public void PayResource(int Cost)
    {
        CurrentResources -= Cost;
        CurrentResources = Math.Max(0, CurrentResources);
    }

    public void GainResource(int Gain)
    {
        CurrentResources += Gain;
        CurrentResources = Math.Min(ResourcesMax, CurrentResources);
    }
}

public class BattleResourcesGroup
{
    public Dictionary<int, BattleResources> BattleRsourceDic;

    public BattleResourcesGroup(Dictionary<int, BattleResources> BattleRsourceDic)
    {
        this.BattleRsourceDic = BattleRsourceDic;
    }

    public bool ResourceGroupCheck(Cost cost)
    {
        foreach(var pair in cost.CostDic)
        {
            if (!BattleRsourceDic.ContainsKey(pair.Key) || !BattleRsourceDic[pair.Key].ResourceCheck(pair.Value))
                return false;
        }
        return true;
    }

    public void PayResourceGroup(Cost cost)
    {
        foreach (var pair in cost.CostDic)
        {
            if (BattleRsourceDic.ContainsKey(pair.Key))
                BattleRsourceDic[pair.Key].PayResource(pair.Value);
        }
    }

    public void GainResourceGroup(Gain gain)
    {
        foreach (var pair in gain.GainDic)
        {
            if (BattleRsourceDic.ContainsKey(pair.Key))
                BattleRsourceDic[pair.Key].GainResource(pair.Value);
        }
    }
}

public class Cost:Ownerable
{
    public Dictionary<int, BattleProperty> CostDic;

    public Cost(Dictionary<int, int> CostDic)
    {
        this.CostDic = new Dictionary<int, BattleProperty>();
        foreach(var pair in CostDic)
        {
            this.CostDic.Add(pair.Key, new BattleProperty(pair.Key + StaticManager.BattlePropertyIDIncreaseDic["消耗"], StaticManager.BattleResourceName[pair.Key] + "消耗", pair.Value));
        }
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        foreach(var cost in CostDic)
        {
            cost.Value.Init(Self);
        }
    }
}

public class Gain:Ownerable
{
    public Dictionary<int, BattleProperty> GainDic;

    public Gain(Dictionary<int, int> CostDic, Unit Self)
    {
        this.GainDic = new Dictionary<int, BattleProperty>();
        foreach (var pair in CostDic)
        {
            this.GainDic.Add(pair.Key, new BattleProperty(pair.Key + StaticManager.BattlePropertyIDIncreaseDic["消耗"], StaticManager.BattleResourceName[pair.Key] + "获取", pair.Value));
        }
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        foreach (var cost in GainDic)
        {
            cost.Value.Init(Self);
        }
    }

}

