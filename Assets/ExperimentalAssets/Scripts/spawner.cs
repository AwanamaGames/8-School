using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour, IInteractable
{
    public List<GameObject> hantu;
    public List<StatSO> hantuSO;
    private int rng;
    private int quantity;

    public void Interact()
    {
        quantity = Random.Range(0, 5);
        
        for (int i = 0; i < quantity; i++){
            rng = Random.Range(0, hantu.Count);
            Instantiate(hantu[rng], Random.insideUnitSphere * 2, Quaternion.identity);
        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
    }

}
