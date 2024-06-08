using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public List<GameObject> hantu;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(hantu[Random.Range(0, 2)], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Instantiate(hantu[Random.Range(0, 2)], transform.position, Quaternion.identity);
        }
    }
}
