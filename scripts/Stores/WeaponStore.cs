using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeaponStore
{
    public static Dictionary<int, Weapon> WeaponDic;

    static WeaponStore()
    {
        WeaponDic = new Dictionary<int, Weapon>();
        Dictionary<DamageType, Penetrate> tempPenetrateDic = new Dictionary<DamageType, Penetrate>();
        tempPenetrateDic.Add(DamageType.Kinect, new Penetrate(DamageType.Kinect, 5, 0, null));
        Weapon tempWeapon = new Weapon(0, "空手", 1f, 1f, 0.75f, 0.75f, new PenetrateGroup(tempPenetrateDic));
        WeaponDic.Add(0, tempWeapon);
    }

    public static Weapon Obtain(int id, Unit Self)
    {
        return (Weapon)WeaponDic[id].Obtain(Self);
    }



}

