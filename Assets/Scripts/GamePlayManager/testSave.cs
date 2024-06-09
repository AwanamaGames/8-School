using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class testSave : MonoBehaviour
{
    public pStatManager playerStat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Save();
        }
        if (Input.GetButtonDown("Fire2")){
            Load();
        }
    }

    public void Save()
    {
        SaveObject saveObject = new SaveObject {
            inventory = playerStat.items
        };

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/save.json", json);

        Debug.Log(Application.dataPath);
    }

    public void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.json");

        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

        playerStat.items = saveObject.inventory;
    }


    public class SaveObject
    {
        public List<ItemList> inventory;
    }
}


