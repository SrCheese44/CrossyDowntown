using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    Vector3 clickInicial;
    Vector3 alSoltarClick;

    public float offset = 100f;
    

    


    public delegate void SwipeControllerDelegate(Vector3 direction);
    public event SwipeControllerDelegate OnSwipe;

    public static SwipeController instance;
    void Awake()
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
        if(Input.GetMouseButtonDown(0)) 
        {
            clickInicial = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            alSoltarClick = Input.mousePosition;
            
            Vector3 diferencia = alSoltarClick - clickInicial;

            
            if(Mathf.Abs(diferencia.magnitude) > offset)
            {
                diferencia = diferencia.normalized;
                diferencia.z = diferencia.y;

                if(Mathf.Abs(diferencia.x)> Mathf.Abs(diferencia.z))
                {
                    diferencia.z = 0.0f;
                }

                else
                {
                    diferencia.x = 0.0f;
                }

                diferencia.y = 0.0f;

                if(OnSwipe != null)
                {
                    OnSwipe(diferencia);
                }
            }


        }
    }


    
}
