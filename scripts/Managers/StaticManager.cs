using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class StaticManager
{
    public static int BattlePropertyCount;

    static StaticManager()
    {
        BattlePropertyCount = 4;
        DamageTypesName = new Dictionary<DamageType, string>();
        DamageTypesName.Add((DamageType)0, "切割");
        DamageTypesName.Add((DamageType)1, "动能");
        DamageTypesName.Add((DamageType)2, "火焰");
        BattleResourceName = new Dictionary<int, string>();
        BattleResourceName.Add(0, "魔法");
        BattleResourceName.Add(1, "精力");
        BattlePropertyIDIncreaseDic = new Dictionary<string, int>();
        BattlePropertyIDIncreaseDic.Add("抗性", 10);
        BattlePropertyIDIncreaseDic.Add("穿透", 20);
        BattlePropertyIDIncreaseDic.Add("消耗", 30);
    }

    public static Dictionary<DamageType, string> DamageTypesName;
    public static Dictionary<int, string> BattleResourceName;
    public static Dictionary<string, int> BattlePropertyIDIncreaseDic;
}

