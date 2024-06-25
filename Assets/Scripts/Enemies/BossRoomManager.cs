using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public Transform center;
    public Transform top;

    public Transform behind;
    public Transform right1;
    public Transform right2;
    public Transform right3;
    public Transform left1;
    public Transform left2;
    public Transform left3;

    public Transform CRight;
    public Transform CTop;
    public Transform CLeft;
    public Transform CDown;
   

    public Vector3 vCenter;
    public Vector3 vTop;
    public Vector3 vRight1;
    public Vector3 vRight2;
    public Vector3 vRight3;
    public Vector3 vLeft1;
    public Vector3 vLeft2;
    public Vector3 vLeft3;

    void Start()
    {
        vCenter = center.position;
        vTop = top.position;
        vRight1 = right1.position;
        vRight1 = right1.position;
        vRight1 = right1.position;
        vLeft1 = left1.position;
        vLeft2 = left1.position;
        vLeft3 = left1.position;
    }
}
