using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    Vector3 s_ClickInicial;
    Vector3 s_AlSoltarClick;
    public float s_Offset = 100f;

    Vector3 s_Click;

    //Events
    public delegate void Movement(Vector3 m_Direction);
    public event Movement onMovement;

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
            s_ClickInicial = Input.mousePosition;
            s_Click = Vector3.forward;

        }

        if (Input.GetMouseButtonUp(0))
        {
            s_AlSoltarClick = Input.mousePosition;
            Vector3 m_Diferencia = s_AlSoltarClick - s_ClickInicial;
            if (Mathf.Abs(m_Diferencia.magnitude) > s_Offset)
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

                if (onMovement != null)
                {
                    onMovement(m_Diferencia);
                }
               
            }
            else
            {
                Vector3 click = s_Click;

                if (onMovement != null)
                {
                    onMovement(click);
                }
            }
        }
    }

}