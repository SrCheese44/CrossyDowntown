using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMov : MonoBehaviour
{
    public PlayerBehaviour m_PlayerBehaviour;
    public SwipeController m_SwipeController;

    public GameObject m_Terrain;

    public bool m_CanMove = true;

    public float m_Duration = 0.25f;
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
        RaycastHit m_Hitinfo = PlayerBehaviour.m_RaycastDirection;

        if (m_PlayerBehaviour != null && m_PlayerBehaviour.m_CanJump && m_CanMove)
        {

            if (Physics.Raycast(m_PlayerBehaviour.transform.position + new Vector3(0, 1f, 0), m_Direction, out m_Hitinfo, 1f))
            {
                if (m_Hitinfo.collider.tag != "ProceduralTerrain")
                {
                    if (m_Direction.z != 0)
                    {
                        m_Direction.z = 0;
                    }
                }


            }
            if (m_Direction.normalized.z >= 0 && m_PlayerBehaviour.m_StepsBack == 0)
            {
                LeanTween.move(m_Terrain, m_Terrain.transform.position + new Vector3(0, 0, -m_Direction.normalized.z), m_Duration).setEase(LeanTweenType.easeOutQuad);
            }


           

        }
    }



    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_PlayerBehaviour.m_CanJump = true;

        }
    }
}
