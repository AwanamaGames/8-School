using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

    public Item item;
    public Items itemDrop;
    [HideInInspector] public StatSO stat;

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
        // Play attack sound if there is one
        if (soundEffectDetails.itemGetSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.itemGetSoundEffect);
        }

        foreach (ItemList i in player.items)
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
            case Items.CursedDollUpperHalf:
                return new CursedDollUpperHalf();
            case Items.CursedDollLowerHalf:
                return new CursedDollLowerHalf();
            case Items.ObatMerah:
                return new ObatMerah();
            case Items.KerisBracelet:
                return new KerisBracelet();

            #region New Items
            case Items.MovSpeedDuration:
                return new MovSpeedDuration();
            case Items.IncreaseDef:
                return new IncreaseDef();
            case Items.DoubleLeaf:
                return new DoubleLeaf();
            #endregion

            default:
                return new ObatMerah();
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
    CursedDollUpperHalf,
    CursedDollLowerHalf,
    KerisBracelet,
    MovSpeedDuration,
    IncreaseDef,
    DoubleLeaf,
    

}
