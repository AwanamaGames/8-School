using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class pStatManager : MonoBehaviour
{
    [SerializeField] FloatingHealthBar healthBar;

    public StatSO stat;
    public LeafCounter LeafCounter;

    //public List<ItemList> items = new List<ItemList>();
    //private List<ItemList> storedItems = new List<ItemList>();

    public List<ItemList> items
    {
        get { return DataHolder.items; }
        set { DataHolder.items = value; }
    }

    public bool isDoubleLeafActive = false;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        LeafCounter = GetComponentInChildren<LeafCounter>();
    }

    async void Start()
    {
        stat = GameManager.Instance.gameplaySO;

        ///KerisBracelet item = new KerisBracelet();
        ///items.Add(new ItemList(item, item.GiveName(), 1));

        StartCoroutine(CallItemUpdate());
    }

    void Update()
    {
        healthBar.UpdateHealthBar((float)stat.currentHP, (float)stat.maxHP);
        LeafCounter.UpdateLeafText(stat.leaf);
    }

    IEnumerator CallItemUpdate()
    {
        foreach (ItemList i in items)
        {
            i.item.Update(this, i.stacks);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CallItemUpdate());
    }

    public void takeDMG(int damage)
    {
        if (damage < 0)
        {
            damage = 0;
        }

        Debug.Log(damage);
        if (damage < 0){damage = 0;}
        stat.currentHP -= damage;

        if (stat.currentHP <= 0)
        {
            GameManager.Instance.ChangeState(GameState.GameLose);
            GameObject.Destroy(gameObject);
        }
    }

    public void CallItemOnHit(hitbox hitbox, StatManager enemy)
    {
        foreach (ItemList i in items)
        {
            i.item.OnHit(this, hitbox, enemy, i.stacks);
        }
    }

    public void CallItemOnDash()
    {
        foreach (ItemList i in items)
        {
            i.item.OnDash(this, i.stacks);
        }
    }

    #region For InventoryUI
    public event Action InventoryChanged; // Event to notify inventory changes

    public void AddItem(ItemList newItem)
    {
        items.Add(newItem);
        InventoryChanged?.Invoke(); // Fire event to notify inventory change
    }

    public void RemoveItem(Item item)
    {
        items.RemoveAll(i => i.item == item);
        InventoryChanged?.Invoke(); // Fire event to notify inventory change
    }
    #endregion
}
