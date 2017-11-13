using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    float stationHealth = 10;
    public BoxCollider triggerCollider;

    public float StationHealth
    {
        get { return stationHealth; }
        set { stationHealth = value; }
    }

    public BoxCollider TriggerCollider
    {
        get { return triggerCollider; }
    }

}

