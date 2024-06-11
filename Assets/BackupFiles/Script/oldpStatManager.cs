//using System.Collections;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;

//public class pStatManager : MonoBehaviour
//{
//    [SerializeField] public StatSO originStat;
//    [SerializeField] FloatingHealthBar healthBar;
//    public StatSO stat;
//    public LeafCounter LeafCounter;

//    public List<ItemList> items = new List<ItemList>();
//    public bool isDoubleLeafActive = false; // Flag to check if double leaf effect is active. used for "double leaf" item


//    private void Awake()
//    {
//        healthBar = GetComponentInChildren<FloatingHealthBar>();
//        LeafCounter = GetComponentInChildren<LeafCounter>();
//    }

//    async void Start()
//    {
//        stat = Instantiate(originStat);

//        StartCoroutine(CallItemUpdate());

//    }

//    void Update()
//    {
//        healthBar.UpdateHealthBar((float)stat.currentHP, (float)stat.maxHP);
//        LeafCounter.UpdateLeafText(stat.leaf);
//    }

//    IEnumerator CallItemUpdate()
//    {
//        foreach (ItemList i in items)
//        {
//            i.item.Update(this, i.stacks);
//        }
//        yield return new WaitForSeconds(1);
//        StartCoroutine(CallItemUpdate());
//    }

//    public void takeDMG(int damage)
//    {
//        Debug.Log(damage);
//        stat.currentHP -= damage;

//        if (stat.currentHP <= 0)
//        {
//            GameObject.Destroy(gameObject);
//        }
//    }

//    public void CallItemOnHit(hitbox hitbox, StatManager enemy)
//    {
//        foreach (ItemList i in items)
//        {
//            i.item.OnHit(this, hitbox, enemy, i.stacks);
//        }
//    }

//    // New method to drop the used item
//    // Currently used in new Items
//    public void RemoveItem(Item item)
//    {
//        items.RemoveAll(i => i.item == item);
//    }

//}
