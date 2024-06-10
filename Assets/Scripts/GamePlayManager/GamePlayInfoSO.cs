using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePlaySO", menuName ="SO/GamePlaySO")]
public class GamePlayInfo : ScriptableObject
{
    public int level;
    public bool tutorial;
}
