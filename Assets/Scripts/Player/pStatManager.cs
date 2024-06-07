using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class pStatManager : MonoBehaviour
{
    [SerializeField] public StatSO originStat;
    public StatSO stat;

    public List<ItemList> items = new List<ItemList>();


    async void Start()
    {   
        stat = Instantiate(originStat);


        StartCoroutine(CallItemUpdate());
        
    }

    void Update()
    {
        
        
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
        Debug.Log(damage);
        stat.currentHP -= damage;
        if (stat.currentHP <= 0)
        {
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

}
