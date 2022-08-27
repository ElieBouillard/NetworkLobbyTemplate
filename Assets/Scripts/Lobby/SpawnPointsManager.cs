using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsManager : Singleton<SpawnPointsManager>
{
    [Space(20)]
    public Vector3[] SpawnPointsCoord;
}
