using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class pStatManager : MonoBehaviour
{
    [SerializeField] FloatingHealthBar healthBar;
    public StatSO stat;
    public LeafCounter LeafCounter;

    public List<ItemList> items = new List<ItemList>();
    public bool isDoubleLeafActive = false;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        LeafCounter = GetComponentInChildren<LeafCounter>();
    }

    async void Start()
    {
        stat = GameManager.Instance.gameplaySO;

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

    public void RemoveItem(Item item)
    {
        items.RemoveAll(i => i.item == item);
    }
}
