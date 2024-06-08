using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/Normal Attack")]
public class attackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float damage;

}
