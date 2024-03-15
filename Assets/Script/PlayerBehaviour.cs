using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
   public SwipeController swipeEvent;

    public GameObject cube;
    public float leanDuration = 0.3f;

    void Start()
    {
        SwipeController.instance.OnSwipe += MoveTarget;

    }


    void OnDestroy()
    {
        SwipeController.instance.OnSwipe -= MoveTarget;

    }


    void MoveTarget(Vector3 direction)
    {


        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }

        if(direction.x < 0) 
        {
            transform.eulerAngles = new Vector3(0f, -90f, 0f);
        }

        LeanTween.move(cube, cube.transform.position + direction / 2 + Vector3.up / 2, leanDuration / 2).setOnComplete(() => 
        { LeanTween.moveLocal(cube, cube.transform.position + direction / 2 - Vector3.up / 2, leanDuration / 2); });

    }
}
