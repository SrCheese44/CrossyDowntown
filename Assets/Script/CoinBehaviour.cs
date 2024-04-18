using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{

    private void Update()
    {      

       transform.Rotate(Vector3.up, Time.deltaTime * 65);

        
    }

   
}
