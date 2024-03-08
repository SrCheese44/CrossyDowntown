using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    Vector3 clickInicial;
    Vector3 alSoltarClick;

    float offset = 100f;

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

        if(Input.GetMouseButtonUp(1)) 
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
                MoveTarget(- cube.GetComponent<Transform>().right); 
            }
            if (diferencia.y < -offset)
            {
                MoveTarget(cube.GetComponent<Transform>().right);
            }
            if (diferencia.x > offset)
            {
                Debug.Log("Se ha movido hacia la dch");
            }
            if (diferencia.y > offset)
            {
                Debug.Log("Se ha movido hacia arriba");
            }



        }
    }


    void MoveTarget(Vector3 direction)
    {
        cube.transform.position += direction;
    }
}
