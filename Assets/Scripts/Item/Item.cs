using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

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

#region NEW ITEMS
public class MovSpeedDuration : Item
{
    private bool isBoostActive = false;
    private int originalSpeed;
    private int extraSpeed = 15; 
    private float duration = 5f;  // Duration for the effect to last

    public override string GiveName()
    {
        return "MovSpeedDuration";
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

        yield return new WaitForSeconds(duration);

        stat.stat.movSpd = originalSpeed; // turn it back to the original move speed
        isBoostActive = false;

        // Remove the item from the list
        stat.RemoveItem(item);
    }
}


public class IncreaseDef : Item
{
    private int extraDef = 5; // 
    private bool defIncreased = false;
    private int currentDef;
    public override string GiveName()
    {
        return "IncreaseDef";
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
        stat.RemoveItem(item);
    }
}

public class DoubleLeaf : Item
{
    private bool isEffectActive = false;
    private float duration = 10f; // Duration for the effect to last

    public override string GiveName()
    {
        return "DoubleLeaf";
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

        yield return new WaitForSeconds(duration);

        stat.isDoubleLeafActive = false; // Deactivate the double leaf effect
        isEffectActive = false;

        // Remove the item from the list
        stat.RemoveItem(this);
    }
}
#endregion