using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public EventCreator eventoEnter;


    public void OnEnable()
    {
        eventoEnter.OnPresionarEnter += HaPresionadoEnter;

    }

    public void OnDisable()
    {
        eventoEnter.OnPresionarEnter -= HaPresionadoEnter;
    }

    private void HaPresionadoEnter()
    {
        Debug.Log("Ha presionado Enter");
    }


}

