using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public Items itemDrop;
    // Start is called before the first frame update
    void Start()
    {
        item = AssigntItem(itemDrop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            pStatManager player = coll.gameObject.GetComponent<pStatManager>();
            AddItem(player);
            Destroy(this.gameObject);
        }
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

            #region New Items
            case Items.MovSpeedDuration:
                return new MovSpeedDuration();
            case Items.IncreaseDef:
                return new IncreaseDef();
            case Items.DoubleLeaf:
                return new DoubleLeaf();
            #endregion

            default:
                return new AirSuci();
        }
    }
}

public enum Items
{
    ObatMerah,
    AirSuci,
    MovSpeedDuration,
    IncreaseDef,
    DoubleLeaf

}
