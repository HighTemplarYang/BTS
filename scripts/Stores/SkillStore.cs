using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SkillStore
{
    public static Dictionary<int, SkillBase> SkillDic;

    static SkillStore()
    {
        //初始化字典
        SkillDic = new Dictionary<int, SkillBase>();
        //添加0号技能挥击
        OriginDamage tempOD = new OriginDamage(DamageType.Cut, new List<float>() { 0.25f, 0.75f, 0, 0 }, 0);
        List<BattleActionBase> tempBAB = new List<BattleActionBase>();
        tempBAB.Add(new DealDamageAction(tempOD,TargetType.EnemyFront));
        tempOD = new OriginDamage(DamageType.Kinect, new List<float>() { 0.75f, 0.25f, 0, 0 }, 0);
        tempBAB.Add(new DealDamageAction(tempOD, TargetType.EnemyFront));
        BattleActionGroup tempBAG = new BattleActionGroup(tempBAB);
        Dictionary<int, int> tempCD = new Dictionary<int, int>();
        tempCD.Add(1, 10);
        Cost tempC = new Cost(tempCD);
        SkillBase tempSB = new SkillBase(0, "挥击", 1, tempBAG, tempC);
    }

}

