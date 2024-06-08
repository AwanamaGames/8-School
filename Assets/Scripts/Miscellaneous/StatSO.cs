using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stat", menuName ="SO/stat")]
public class StatSO : ScriptableObject
{   
    public int maxHP;
    public int currentHP;
    public int att;
    public int movSpd;
    public int attSpd;
    public int def;
    public bool isAgro;

    public int leaf;
}
