using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceProp : MonoBehaviour
{
    public RandomPrefabSpawner m_RandomPrefabSpawner;
    public Collider m_Trigger; // El trigger donde se detectarán las salidas del objeto con el tag "prop"

    private void Start()
    {
        m_RandomPrefabSpawner.SpawnRandomPrefab();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            // Si el objeto que sale del trigger es un "prop" y es el mismo trigger seleccionado, generar un nuevo objeto
            m_RandomPrefabSpawner.SpawnRandomPrefab();
        }
    }

}