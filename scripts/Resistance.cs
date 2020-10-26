using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Resistance:Ownerable
{
    public DamageType ResistanceType;
    public BattleProperty ResistanceRate;
    public BattleProperty ResistanceStatic;
    public Resistance(DamageType ResistanceType)
    {
        new Resistance(ResistanceType, 0, 0);
    }
    public Resistance(DamageType ResistanceType, float ResistanceRate, float ResistanceStatic)
    {
        this.ResistanceType = ResistanceType;
        this.ResistanceRate = new BattleProperty(10 + (int)ResistanceType, StaticManager.DamageTypesName[ResistanceType] + "抗性比率", ResistanceRate);
        this.ResistanceStatic = new BattleProperty(20 + (int)ResistanceType, StaticManager.DamageTypesName[ResistanceType] + "抗性减免", ResistanceStatic);
        this.Self = Self;
    }

    public float DamageCheck(FinnalDamage finnalDamage)
    {
        float resistanceRate = ResistanceRate.FixedValue - finnalDamage.penetrate.PenetrateRate;
        resistanceRate = Math.Max(0, resistanceRate);
        resistanceRate /= 100f;
        float resistanceStatic = ResistanceStatic.FixedValue - finnalDamage.penetrate.PenetrateStatic;
        resistanceStatic = Math.Max(0, resistanceStatic);
        return finnalDamage.damage * (1 - resistanceRate) - resistanceStatic;
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        ResistanceRate.Init(Self);
        ResistanceStatic.Init(Self);
    }
}

public class ResistanceGroup:Ownerable
{
    private Dictionary<DamageType, Resistance> resistanceDic;

    public ResistanceGroup(Dictionary<DamageType, Resistance> resistanceDic,Unit Self)
    {
        resistanceDic = new Dictionary<DamageType, Resistance>();
        for (int i = 0; i < 10; i++)
        {
            if(resistanceDic.TryGetValue((DamageType)i,out Resistance resistance))
            {
                this.resistanceDic.Add((DamageType)i, resistance);
            }
            else
            {
                this.resistanceDic.Add((DamageType)i, new Resistance((DamageType)i));
            }
        }
    }

    public Resistance GetResistance(DamageType damageType)
    {
        return resistanceDic[damageType];
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        foreach(var pair in resistanceDic)
        {
            pair.Value.Init(Self);
        }
    }
}

public class Penetrate:Ownerable
{
    public DamageType PenetrateType;
    public BattleProperty PenetrateRate;
    public BattleProperty PenetrateStatic;

    public Penetrate(DamageType PenetrateType)
    {
        new Penetrate(PenetrateType, 0, 0, Self);
    }

    public Penetrate(DamageType PenetrateType, float PenetrateRate, float PenetrateStatic,Unit Self)
    {
        this.PenetrateType = PenetrateType;
        this.PenetrateRate = new BattleProperty(30 + (int)PenetrateType, StaticManager.DamageTypesName[PenetrateType] + "穿透比率", PenetrateRate);
        this.PenetrateStatic = new BattleProperty(40 + (int)PenetrateType, StaticManager.DamageTypesName[PenetrateType] + "固定穿透", PenetrateStatic);
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        PenetrateRate.Init(Self);
        PenetrateStatic.Init(Self);
    }
}

public class PenetrateGroup:Ownerable
{
    private Dictionary<DamageType, Penetrate> penetrateDic;

    public PenetrateGroup(Dictionary<DamageType, Penetrate> penetrateDic)
    {
        penetrateDic = new Dictionary<DamageType, Penetrate>();
        for (int i = 0; i < 10; i++)
        {
            if (penetrateDic.TryGetValue((DamageType)i, out Penetrate penetrate))
            {
                this.penetrateDic.Add((DamageType)i, penetrate);
            }
            else
            {
                this.penetrateDic.Add((DamageType)i, new Penetrate((DamageType)i));
            }
        }
    }



    public Penetrate GetPenetrate(DamageType damageType)
    {
        return penetrateDic[damageType];
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
        foreach(var pair in penetrateDic)
        {
            pair.Value.Init(Self);
        }
    }
}

