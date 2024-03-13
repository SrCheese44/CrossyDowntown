using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    Vector3 clickInicial;
    Vector3 alSoltarClick;

    public float offset = 100f;
    public float leanDuration = 0.3f;

    public GameObject cube;
    void Start()
    {
        
    }

    // Update is called once per frame
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
            Debug.Log(diferencia);

            ///Si X es negativa, izq
            ///Si X es positiva, dch
            ///si y es negativa baja
            ///si y es positiva sube
            
            if(diferencia.x < -offset)
            {
                MoveTarget(-cube.GetComponent<Transform>().right); 
            }
            if (diferencia.y < -offset)
            {
                MoveTarget(-cube.GetComponent<Transform>().forward);
            }
            if (diferencia.x > offset)
            {
                Debug.Log("Se ha movido hacia la dch");
                MoveTarget(cube.GetComponent<Transform>().right);

            }
            if (diferencia.y > offset)
            {
                Debug.Log("Se ha movido hacia arriba");
                MoveTarget(cube.GetComponent<Transform>().forward);
            }


        }
    }


    void MoveTarget(Vector3 direction)
    {
        LeanTween.move(cube, cube.transform.position + direction,leanDuration).setEase(LeanTweenType.easeOutQuad);
    }
}
