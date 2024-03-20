using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneratorManager : MonoBehaviour
{
    [SerializeField]
    public GameObject m_TerrainPrefab;
    public int m_Nterrain;
    public GameObject m_TerrainSpawn;
    public GameObject m_InicialTerrainSpawn;
    public GameObject[] m_InicialTerrains;
    public GameObject m_proceduralSpawn;
    public GameObject[] m_ProceduralTerrains;


    void Start()
    {
        int m_RandomIndex = Random.Range(0, m_InicialTerrains.Length);
        ObjectPool.PreLoad(m_TerrainPrefab, m_Nterrain);
        m_InicialTerrains[m_RandomIndex].transform.position = m_InicialTerrainSpawn.transform.position;
        m_ProceduralTerrains[m_RandomIndex].transform.position = m_proceduralSpawn.transform.position;
    }

    public void RecycleTerrain(GameObject m_Terrain)
    {
        // Reciclar el terreno utilizando el ObjectPool
        ObjectPool.RecycleObject(m_TerrainPrefab, m_Terrain);
    }

    public void NewLevelZone()
    {
        GameObject m_Terrain = ObjectPool.GetObject(m_TerrainPrefab);
        m_Terrain.transform.position = m_TerrainSpawn.transform.position;
    }
}