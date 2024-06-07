using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public abstract class Item
{   
   public abstract string GiveName();
   public virtual void Update(pStatManager stat, int stacks)
   {

   }

   public virtual void OnHit(pStatManager stat, hitbox hitbox,StatManager enemy, int stacks)
   {

   }

}

public class ObatMerah : Item
{
    public override string GiveName()
    {
        return "Obat Merah";
    }
    public override void Update(pStatManager stat, int stacks)
    {
        stat.stat.currentHP += 2 * stacks; 
    }
}

public class AirSuci : Item
{
    public override string GiveName()
    {
        return "Air Suci";
    }
    public override void OnHit(pStatManager stat, hitbox hitbox, StatManager enemy, int stacks)
    {
        if (enemy.stat.currentHP / enemy.stat.maxHP >= 0.5)
        {
            hitbox.totalDamage = (int)(hitbox.totalDamage * 1.5);
        }
    }
}
