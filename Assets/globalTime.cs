using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalTime : MonoBehaviour
{
    public float global;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        global = Time.time;
    }
}
