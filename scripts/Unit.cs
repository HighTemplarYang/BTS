using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CampType
{
    Allie,
    Enemy,
    Neutral
}

public class Unit 
{
    //单位阵营信息
    public CampType campType;

    public FixGroup fixGroup;

    //单位是否存活
    private bool alive;
    public bool Alive
    {
        get {
            return alive;
        }
    }

    //单位的剩余生命值
    private float life;
    public float Life
    {
        get
        {
            return life;
        }
    }

    //单位的战斗资源
    public BattleResourcesGroup BattleResourcesGroup;
    //单位的基础抗性
    public ResistanceGroup resistanceGroup;

    //单位的基础属性
    public List<BattleProperty> BattlePropertys;

    //单位的武器
    public Weapon weapon;
    //单位的BUFF机
    public BuffMachine buffMachine;

    //获取单位的穿透属性
    public Penetrate GetPenetrate(DamageType damageType)
    {
        return weapon.penetrateGroup.GetPenetrate(damageType);
    }
    

    public Unit(CampType campType,ResistanceGroup resistanceGroup,int life)
    {
        BattlePropertys = new List<BattleProperty>();
        BattlePropertys.Add(new BattleProperty(0, "力量", 10));
        BattlePropertys.Add(new BattleProperty(1, "敏捷", 10));
        BattlePropertys.Add(new BattleProperty(2, "智力", 10));
        BattlePropertys.Add(new BattleProperty(3, "信仰", 10));
        foreach(var property in BattlePropertys)
        {
            property.Init(this);
        }
        weapon = WeaponStore.Obtain(0, this);
    }

    //计算单位受到的最终伤害
    public FinnalDamage CorrectDamage(OriginDamage originDamage)
    {
        float Damage = 0;
        for(int i = 0; i < StaticManager.BattlePropertyCount; i++)
        {
            Damage += originDamage.DamageRateList[i] * BattlePropertys[i].FixedValue * weapon.WeaponCorrects[i];
        }
        Damage += originDamage.DamageStatic;
        return new FinnalDamage(originDamage.damageType, Damage, GetPenetrate(originDamage.damageType));
    }

    //单位获得BUFF
    public bool SetBuff(Buff buff)
    {
        buffMachine.SetBuff(buff);
        return true;
    }

    //单位受到伤害
    public void TakeDamage(FinnalDamage finnalDamage)
    {
        float TakedNumber = resistanceGroup.GetResistance(finnalDamage.damageType).DamageCheck(finnalDamage);
        LoseLife(TakedNumber);
    }

    //单位失去生命
    public void LoseLife(float lifeNum)
    {
        life -= lifeNum;
    }

}

