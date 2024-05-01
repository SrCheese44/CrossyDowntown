using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDespawner : MonoBehaviour
{
    public Prop1Spawner initialPropSpawn;
    public RandomPrefabSpawner spawnProp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InitialProp"))
        {
            
            initialPropSpawn.inactiveObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent = null;
        }
        if (other.CompareTag("Prop"))
        {
            spawnProp.inactiveObjectsMiddle.Add(other.gameObject);
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent = null;
        }
    }
}