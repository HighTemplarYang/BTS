using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BattleProperty:Ownerable
{
    public int ID;
    public string Name;
    public float value;

    public BattleProperty(int ID,string Name,float Value)
    {
        this.ID = ID;
        this.Name = Name;
        this.value = Value;
    }

    public float FixedValue
    {
        get
        {
            float tempvalue = value;
            Fix tempfix = Self.fixGroup.GetFix(ID);
            return ((value * (1 + tempfix.AddCorrectRate)) + tempfix.BaseCorrectStatic) * tempfix.MultiCorrectRate + tempfix.AdditionCorrectStatic;
        }
    }

    public override void Init(Unit Self)
    {
        base.Init(Self);
    }

    public static implicit operator float( BattleProperty battleProperty)
    {
        return battleProperty.FixedValue;
    }

    public static implicit operator int(BattleProperty battleProperty)
    {
        return (int)Math.Round(battleProperty.FixedValue);
    }

}

