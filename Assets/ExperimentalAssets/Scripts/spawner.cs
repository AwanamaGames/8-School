using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour, IInteractable
{
    public List<GameObject> hantu;
    public List<StatSO> hantuSO;
    public Tilemap tilemap;
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

            BoundsInt bounds = tilemap.cellBounds;

            Vector3Int randomCellPosition = new Vector3Int(
                Random.Range(bounds.min.x, bounds.max.x + 1),
                Random.Range(bounds.min.y, bounds.max.y + 1),
                bounds.min.z);

            // Convert the cell position to world position
            Vector3 spawnPosition = tilemap.GetCellCenterWorld(randomCellPosition);

            Instantiate(hantu[rng], spawnPosition, Quaternion.identity);
        }
    }

    public void SpawnLite()
    {
        quantity = Random.Range(1, 3);

        for (int i = 0; i < quantity; i++)
        {
            rng = Random.Range(0, hantu.Count);

            BoundsInt bounds = tilemap.cellBounds;

            Vector3Int randomCellPosition = new Vector3Int(
                Random.Range(bounds.min.x, bounds.max.x + 1),
                Random.Range(bounds.min.y, bounds.max.y + 1),
                bounds.min.z);

            // Convert the cell position to world position
            Vector3 spawnPosition = tilemap.GetCellCenterWorld(randomCellPosition);

            Instantiate(hantu[rng], spawnPosition, Quaternion.identity);
        }
    }

    public void Interact()
    {
        Spawn();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (tilemap == null)
        {
            tilemap = FindObjectOfType<Tilemap>();
        }

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
