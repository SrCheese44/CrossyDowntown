using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop1Spawner : MonoBehaviour
{
    public List<GameObject> objectsList;
    public List<GameObject> inactiveObjects = new List<GameObject>();
    public GameObject activeObject;

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject propParent;

    private int propsActivated = 0;

    [SerializeField] GameObject middleSpawnProp;

    private void Start()
    {
        foreach (GameObject prefab in objectsList)
        {
            prefab.SetActive(false);
            inactiveObjects.Add(prefab);
        }

        SpawnRandomPrefab();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == activeObject)
        {
            SpawnRandomPrefab();
        }
    }

    public void SpawnRandomPrefab()
    {
        if (propsActivated < 7 && propsActivated > 5)
        {
            this.enabled = false;
            middleSpawnProp.SetActive(true);
        }
        else
        {
            if (inactiveObjects.Count > 0)
            {
                int randomIndex = Random.Range(0, inactiveObjects.Count);

                activeObject = inactiveObjects[randomIndex];
                activeObject.SetActive(true);

                activeObject.transform.position = spawnPoint.transform.position;

                inactiveObjects.RemoveAt(randomIndex);

                activeObject.transform.parent = propParent.transform;

                propsActivated++;
            }

        }

    }
}