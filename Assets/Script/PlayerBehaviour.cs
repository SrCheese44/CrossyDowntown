using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public SwipeController m_SwipeController;

    public Rigidbody rb;
    [SerializeField]
    SkinnedMeshRenderer mesh;

    public LevelPedazo levelPedazo;

    public int m_StepsBack;

    public float m_Offset = 100f;
    public float m_Duration = 1f;
    public GameObject m_Player;
    public bool m_CanJump = true;

    
    public static RaycastHit m_RaycastDirection; 
    public void Awake()
    {
        m_Player = this.gameObject;
    }

    public void Start()
    {
        m_SwipeController.OnMovement += MoveTarget;
    }


    public void OnDisable()
    {
        m_SwipeController.OnMovement -= MoveTarget;
    }

    void MoveTarget(Vector3 m_Direction)
    {
        if (m_CanJump)
        {
            RaycastHit m_Hitinfo;
            Vector3 m_MoveDirection = m_Direction.normalized;


            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), m_MoveDirection, out m_Hitinfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");
                Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, 1) * m_Hitinfo.distance, Color.red);

                m_RaycastDirection = m_Hitinfo;

                if (m_MoveDirection.x != 0)
                {
                    m_MoveDirection.x = 0;
                }

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, 1) * 20f, Color.green);
            }

            if (m_MoveDirection != Vector3.zero)
            {
                if (m_MoveDirection.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                }
                else if (m_MoveDirection.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, -90f, 0);
                }
                else if (m_MoveDirection.z > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (m_MoveDirection.z < 0)
                {
                    transform.eulerAngles = new Vector3(0, 180f, 0);
                }

                LeanTween.move(m_Player, m_Player.transform.position + new Vector3(m_MoveDirection.x / 2, 0, 0) + Vector3.up / 2, m_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    LeanTween.move(m_Player, m_Player.transform.position + new Vector3(m_MoveDirection.x / 2, 0, 0) - Vector3.up / 2, m_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                });
                
                if (m_StepsBack < 4 && m_Direction.normalized.z <= 0)
                {
                    m_StepsBack++;
                    LeanTween.move(m_Player, m_Player.transform.position + new Vector3(m_MoveDirection.x / 2, 0, m_MoveDirection.z / 2) + Vector3.up / 2, m_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(m_Player, m_Player.transform.position + new Vector3(m_MoveDirection.x / 2, 0, m_MoveDirection.z / 2) - Vector3.up / 2, m_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }
                if (m_StepsBack != 0 && m_Direction.normalized.z >= 0)
                {
                    m_StepsBack--;
                    LeanTween.move(m_Player, m_Player.transform.position + new Vector3(m_MoveDirection.x / 2, 0, m_MoveDirection.z / 2) + Vector3.up / 2, m_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(m_Player, m_Player.transform.position + new Vector3(m_MoveDirection.x / 2, 0, m_MoveDirection.z / 2) - Vector3.up / 2, m_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }

                m_CanJump = false;
            }
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("ProceduralTerrain"))
        {
            m_CanJump = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameUI.instance.coinAmount += 1;
            other.gameObject.SetActive(false);
            GameUI.instance.DisplayText();
        }

        if (other.gameObject.CompareTag("Death"))
        {
            GameUI.instance.GameEnding();
            this.gameObject.SetActive(false);
            SwipeController.instance.enabled = false;
        }
    }
}