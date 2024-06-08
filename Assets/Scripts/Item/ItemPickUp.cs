using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public Item item;
    public Items itemDrop;
    public int price;
    // Start is called before the first frame update
    void Start()
    {
        item = AssigntItem(itemDrop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddItem(pStatManager player)
    {
        foreach(ItemList i in player.items)
        {
            if (i.name == item.GiveName())
            {
                i.stacks += 1;
                return;
            }

        }
        player.items.Add(new ItemList(item, item.GiveName(), 1));
    }

    public Item AssigntItem(Items itemToAssign)
    {
        switch (itemToAssign)
        {
            case Items.AirSuci:
                return new AirSuci();
            case Items.ObatMerah:
                return new ObatMerah();
            default:
                return new AirSuci();
        }
    }

    public void Interact()
    {
        pStatManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>();
        if (player.stat.leaf >= price)
        {
            player.stat.leaf -= price;
            AddItem(player);
            Destroy(this.gameObject);
        }
        
    }
}

public enum Items
{
    ObatMerah,
    AirSuci,

}


