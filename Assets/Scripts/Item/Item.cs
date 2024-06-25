using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
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

   public virtual void OnDash(pStatManager stat, int stacks)
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
        if (stat.stat.currentHP < stat.stat.maxHP)
        {
            stat.stat.currentHP += 3 * stacks; 
        }
    }
}

public class CursedDollUpperHalf : Item
{
    public override string GiveName()
    {
        return "Cursed Doll Upper Half";
    }
    public override void OnHit(pStatManager stat, hitbox hitbox, StatManager enemy, int stacks)
    {
        if (enemy.stat.currentHP / enemy.stat.maxHP >= 0.5)
        {
            hitbox.totalDamage = (int)(hitbox.totalDamage * (1 + 0.1 * stacks));
        }
    }
}

public class CursedDollLowerHalf : Item
{
    public override string GiveName()
    {
        return "Cursed Doll Lower Half";
    }
    public override void OnHit(pStatManager stat, hitbox hitbox, StatManager enemy, int stacks)
    {
        if (enemy.stat.currentHP / enemy.stat.maxHP <= 0.5)
        {
            hitbox.totalDamage = (int)(hitbox.totalDamage * (1 + 0.1 * stacks));
        }
    }
}

public class KerisBracelet : Item
{
    GameObject effect;
    public override string GiveName()
    {
        return "Keris Bracelet";
    }
    public override async void OnDash(pStatManager stat, int stacks)
    {
        if (effect == null) {effect = (GameObject)Resources.Load("Circle", typeof(GameObject));}
        for (int i = 0; i < stacks; i++) 
        {
            GameObject circle = GameObject.Instantiate(effect, stat.transform.position, quaternion.identity);
            await Task.Delay(500);
        }
    }
}

#region NEW ITEMS
public class MovSpeedDuration : Item
{
    private bool isBoostActive = false;
    private int originalSpeed;
    private int extraSpeed = 5;

    public override string GiveName()
    {
        return "Speatu Angin";
    }

    public override void Update(pStatManager stat, int stacks)
    {
        if (!isBoostActive)
        {
            stat.StartCoroutine(TemporarySpeedBoost(stat, this)); // Use coroutine and bool condition to handle update method
        }
    }

    private IEnumerator TemporarySpeedBoost(pStatManager stat, Item item)
    {
        isBoostActive = true;
        originalSpeed = stat.stat.movSpd;
        stat.stat.movSpd =+ extraSpeed; // Boost the move speed

        yield return null;

        //stat.stat.movSpd = originalSpeed; // turn it back to the original move speed
        //isBoostActive = false;

        //// Remove the item from the list
        //stat.RemoveItem(item);
    }
}


public class IncreaseDef : Item
{
    private int extraDef = 20; // 
    private bool defIncreased = false;
    private int currentDef;
    public override string GiveName()
    {
        return "Jubah Hitam";
    }
    public override void Update(pStatManager stat, int stacks)
    {
        if (!defIncreased)
        {
            stat.StartCoroutine(IncreaseDefStat(stat, this)); // Use coroutine and bool condition to handle update method
        }
    }

    private IEnumerator IncreaseDefStat(pStatManager stat, Item item)
    {
        currentDef = stat.stat.def;

        defIncreased = true;

        yield return null;

        stat.stat.def = currentDef + extraDef;

        defIncreased = false;

        // Remove the item from the list
        //stat.RemoveItem(item);
    }
}

public class DoubleLeaf : Item
{
    private bool isEffectActive = false;
    private float duration = 10f; // Duration for the effect to last

    public override string GiveName()
    {
        return "Tangan Pengganda";
    }

    public override void Update(pStatManager stat, int stacks)
    {
        if (!isEffectActive)
        {
            stat.StartCoroutine(DoubleLeafEffect(stat)); // Use coroutine and bool condition to handle update method
        }
    }

    private IEnumerator DoubleLeafEffect(pStatManager stat)
    {
        isEffectActive = true;
        stat.isDoubleLeafActive = true; // Activate the double leaf effect

        yield return null;

        //stat.isDoubleLeafActive = false; // Deactivate the double leaf effect
        //isEffectActive = false;

        //// Remove the item from the list
        //stat.RemoveItem(this);
    }
}
#endregion