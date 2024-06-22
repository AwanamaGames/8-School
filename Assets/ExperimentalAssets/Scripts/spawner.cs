using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour, IInteractable
{
    public List<GameObject> hantu;
    public List<StatSO> hantuSO;
    public int rng;

    public void Interact()
    {
        rng = Random.Range(0, hantu.Count);
        Instantiate(hantu[rng], Random.insideUnitSphere * 2, Quaternion.identity);
        StatUp(rng);

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void StatUp(int rng)
    {
        hantuSO[rng].maxHP += 10;
        hantuSO[rng].currentHP += 10;
        hantuSO[rng].att += 8;
        hantuSO[rng].def += 1;
        hantuSO[rng].leaf += 5;
    }
}
