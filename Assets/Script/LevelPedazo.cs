using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPedazo : MonoBehaviour
{
    public SwipeController m_SwipeController;
    public TerrainGeneratorManager m_TerrainGeneratorManager;
    public PlayerBehaviour m_PlayerBehaviour;

    public float m_Offset = 100f;
    public float m_Duration = 1f;
    public GameObject m_Terrain;
    public bool m_CanMove = true;
    public int m_StepsCounter;

    private bool m_IsRecycled = false;

    public int m_Counter = 0;


    public void Awake()
    {
        m_Terrain = this.gameObject;
    }


    public void OnEnable()
    {
        m_SwipeController.OnMovement += MoveTarget;
    }

    public void OnDisable()
    {
        m_SwipeController.OnMovement -= MoveTarget;
    }

    void MoveTarget(Vector3 m_Direction)
    {
        if (m_PlayerBehaviour != null && m_PlayerBehaviour.m_CanJump &&  m_CanMove)
        {
            LeanTween.move(m_Terrain, m_Terrain.transform.position + new Vector3(0, 0, -m_Direction.normalized.z), m_Duration).setEase(LeanTweenType.easeOutQuad);
            Debug.Log(m_StepsCounter);
            if (m_Direction.normalized.z == 1)
            {
                m_StepsCounter++;
            }
            if (m_Direction.normalized.z == -1)
            {
                m_StepsCounter--;
            }
        }
    }

    public void Update()
    {
        if (m_Counter == 2 && m_IsRecycled == true)
        {
            m_Counter = 0;
            m_IsRecycled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_CanMove = false;
        }
    }
}
