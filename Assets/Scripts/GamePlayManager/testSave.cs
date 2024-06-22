using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class testSave : MonoBehaviour
{
    public pStatManager playerStat;
    public Transform playerLocation;
    // Start is called before the first frame update
    void Start()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>();
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetButtonDown("Fire1")){
    //        Save();
    //    }
    //    if (Input.GetButtonDown("Fire2")){
    //        Load();
    //    }
    //}

    public void SaveItem()
    {
        SaveObject saveObject = new SaveObject {
            inventory = playerStat.items
        };

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.persistentDataPath + "/ItemSave.json", json);

        
    }

    public void LoadItem()
    {
        string saveString = File.ReadAllText(Application.persistentDataPath + "/ItemSave.json");

        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

        playerStat.items = saveObject.inventory;
    }

    public void SaveLocation()
    {
        SaveObject saveObject = new SaveObject {
            transform = playerLocation
        };

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.persistentDataPath + "/LocationSave.json", json);

        
    }
    public void LoadLocation()
    {
        string saveString = File.ReadAllText(Application.persistentDataPath + "/LocationSave.json");

        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

        playerLocation.position = transform.position;
    }


    public class SaveObject
    {
        public List<ItemList> inventory;
        public Transform transform;
    }
}


