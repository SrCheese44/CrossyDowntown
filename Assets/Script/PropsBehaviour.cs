using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PropsBehaviour : MonoBehaviour
{
    public RandomPrefabSpawner pb_SpawnProps;
    public PlayerBehaviour pb_PlayerBehaviour;
    public SwipeController pb_SwipeController;

    [SerializeField] GameObject pb_Player;
    [SerializeField] GameObject pb_Prop;

    public float pb_Duration = 0.25f;


    public void Awake()
    {
        pb_Prop = this.gameObject;
    }

    public void OnEnable()
    {
        pb_SwipeController.OnMovement += MoveTarget;
    }

    public void OnDisable()
    {
        pb_SwipeController.OnMovement -= MoveTarget;
    }

    public void MoveTarget(Vector3 pb_Direction)
    {
        RaycastHit pb_HitInfo = PlayerBehaviour.m_RaycastDirection;

        if (pb_PlayerBehaviour.m_CanJump)
        {
            if (Physics.Raycast(pb_Player.transform.position + new Vector3(0, 1f, 0), pb_Direction, out pb_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");
                if (pb_HitInfo.collider.tag != "ProceduralTerrain")
                {
                    if (pb_Direction.z != 0)
                    {
                        pb_Direction.z = 0;
                    }
                }

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * pb_HitInfo.distance, Color.red);
            }

            if (pb_Direction != Vector3.zero)
            {
                print("Se mueve");
                LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Direction.normalized.z), pb_Duration).setEase(LeanTweenType.easeOutQuad);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnProps"))
        {
            print("Sale");
            pb_SpawnProps.SpawnRandomPrefab();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Recycle"))
        {
            this.gameObject.SetActive(false);
            //pb_SpawnProps.SpawnRandomObject.Add(this.gameObject);
        }
    }
}