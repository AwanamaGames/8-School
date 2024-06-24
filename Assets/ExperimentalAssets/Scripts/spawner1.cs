using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerTest : MonoBehaviour, IInteractable
{
    public List<GameObject> hantu;
    public List<StatSO> hantuSO;
    private int rng;
    private int quantity;

    [SerializeField] private float cooldown;
    private float count;
    [SerializeField] private float lowestCooldown;

    public void Spawn()
    {
        quantity = Random.Range(1, 5);

        for (int i = 0; i < quantity; i++)
        {
            rng = Random.Range(0, hantu.Count);


            Instantiate(hantu[rng], Random.insideUnitCircle * 0.3f, Quaternion.identity);
        }
    }

    public void SpawnLite()
    {
        quantity = Random.Range(1, 3);

        for (int i = 0; i < quantity; i++)
        {
            rng = Random.Range(0, hantu.Count);


            Instantiate(hantu[rng], Random.insideUnitCircle * 0.3f, Quaternion.identity);
        }
    }

    public void Interact()
    {
        Spawn();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        count = Time.deltaTime;
        if (count >= cooldown)
        {
            count = 0;
            SpawnLite();
            if (cooldown > lowestCooldown)
            {
                cooldown *= 0.9f;
            }
        }
    }
}
