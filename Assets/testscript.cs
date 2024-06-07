using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class testscript : MonoBehaviour
{
    float angle;
    [SerializeField] private Camera camera; 
    // Update is called once per frame

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Vector3 test = camera.ScreenToWorldPoint(Input.mousePosition) - camera.transform.position;
            Debug.Log(camera.ScreenToWorldPoint(Input.mousePosition) - camera.transform.position);
            
            angle = Mathf.Atan2 (test.y, test.x) * Mathf.Rad2Deg;
            Debug.Log(angle);
        }
        
    }
}
