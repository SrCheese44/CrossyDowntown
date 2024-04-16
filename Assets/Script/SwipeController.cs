using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    Vector3 m_ClickInicial;
    Vector3 m_AlSoltarClick;
    public float m_Offset = 100f;

    Vector3 s_Click;

    //Events
    public delegate void Movement(Vector3 m_Direction);
    public event Movement OnMovement;

    //Instance
    public static SwipeController instance;

    private void Awake()
    {
        if (SwipeController.instance == null)
        {
            SwipeController.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_ClickInicial = Input.mousePosition;
            s_Click = Vector3.forward;

        }

        if (Input.GetMouseButtonUp(0))
        {
            m_AlSoltarClick = Input.mousePosition;
            Vector3 m_Diferencia = m_AlSoltarClick - m_ClickInicial;
            if (Mathf.Abs(m_Diferencia.magnitude) > m_Offset)
            {
                m_Diferencia = m_Diferencia.normalized;
                m_Diferencia.z = m_Diferencia.y;

                if (Mathf.Abs(m_Diferencia.x) > Mathf.Abs(m_Diferencia.z))
                {
                    m_Diferencia.z = 0.0f;
                }
                else
                {
                    m_Diferencia.x = 0.0f;
                }

                m_Diferencia.y = 0.0f;

                if (OnMovement != null)
                {
                    OnMovement(m_Diferencia);
                }
               
            }
            else
            {
                Vector3 click = s_Click;

                if (OnMovement != null)
                {
                    OnMovement(click);
                }
            }
        }
    }

}