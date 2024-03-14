using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCreator : MonoBehaviour
{
   
    public delegate void PresionaEnter();
    public event PresionaEnter OnPresionarEnter;


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
           if(OnPresionarEnter != null)
            {
                OnPresionarEnter();
            }
        }
    }
}
