using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum TargetType
{
    Self=0,
    AllieFront=0b1,
    AllieMid=0b10,
    AllieBack=0b100,
    AllieAll=0b111,
    EnemyFront=0b1000,
    EnemyMid=0b10000,
    EnemyBack=0b100000,
    EnemyAll=0b111000,
    All=0b111111
}

public class UnitManager:Singleton<UnitManager>
{
    public List<Unit> Enemys;
    public List<Unit> Allies;

    public List<Unit> GetUnits(Unit Self,TargetType targetType)
    {
        
        List<Unit> tempAllie = Self.campType == CampType.Allie ? Allies : Enemys;
        List<Unit> tempEnemy = Self.campType == CampType.Allie ? Allies : Enemys;
        List<Unit> result = new List<Unit>();
        if (targetType == TargetType.Self)
        {
            result.Add(Self);
            return result;
        }
        for (int i = 0; i < tempAllie.Count; i++)
        {
            if (((int)targetType & (1 << i)) != 0)
            {
                result.Add(tempAllie[i]);
            }
        }
        for(int i = 0; i < tempEnemy.Count; i++)
        {
            if (((int)targetType & (0b1000 << i)) != 0)
            {
                result.Add(tempEnemy[i]);
            }
        }
        return result;
    }

}

