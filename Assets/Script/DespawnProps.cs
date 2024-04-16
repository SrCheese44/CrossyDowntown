
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnProps : MonoBehaviour
{
    public RandomPrefabSpawner rp_SpawnProps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            // Si el objeto que entra en el otro collider es un "prop", a�adirlo a la lista de prefabs a spawnear
            rp_SpawnProps.inactiveObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent = null;
        }
    }
}