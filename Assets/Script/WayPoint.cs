using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPoint 
{
    public string TargetEnemyId;
    public List<Transform> listPoints;

    public WayPoint() { }
}
