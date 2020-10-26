using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Cut,
    Kinect,
    Fire
}

public class OriginDamage
{
    public DamageType damageType;
    public List<float> DamageRateList;
    public float DamageStatic;


    public OriginDamage(DamageType damageType, List<float> DamageRateList, float DamageStatic)
    {
        this.damageType = damageType;
        this.DamageRateList = DamageRateList;
        this.DamageStatic = DamageStatic;
    }
}

public class FinnalDamage
{
    public DamageType damageType;
    public float damage;
    public Penetrate penetrate;

    public FinnalDamage(DamageType damageType, float damage, Penetrate penetrate)
    {
        this.damageType = damageType;
        this.damage = damage;
        this.penetrate = penetrate;
    }
}
