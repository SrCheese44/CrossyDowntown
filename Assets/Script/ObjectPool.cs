using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    private static ObjectPool instance;
    //Diccionario encargado de la cola de objetos
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
    //Diccionario en el cual los objetos de la cola se ordenanbb
    static Dictionary<int, GameObject> fillPool = new Dictionary<int, GameObject>();

    //Singleton
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //Esta clase se encarga de cargar el objeto antes de que sea necesario
    public static void PreLoad(GameObject Prefab, int amount)
    {
        //almacenamos la id del objeto para identificarlo mas tarde
        int id = Prefab.GetInstanceID();
        //Creamos el objeto que ordenará la jerarquía
        GameObject category = new GameObject();
        category.name = Prefab.name + "Pool";
        //Se añade a la jerarquía
        fillPool.Add(id, category);
        //Guardamos el objeto en una cola nueva que creamos
        pool.Add(id, new Queue<GameObject>());

        for (int i = 0; i < amount; i++)
        {
            CreateObject(Prefab);
        }
    }
    //Pasamos el objeto original, lo clonaremos
    static void CreateObject(GameObject Prefab)
    {
        int id = Prefab.GetInstanceID();
        //clonamos el objeto
        GameObject prefabCopy = Instantiate(Prefab) as GameObject;
        //le pasamos el id del padre para hacerlo hijo
        prefabCopy.transform.SetParent(GetParent(id).transform);
        prefabCopy.SetActive(false);
        //lo metemos en el diccionario llamado pool
        pool[id].Enqueue(prefabCopy);
    }
    //Se devuelve el ID del padre
    static GameObject GetParent(int parentID)
    {
        GameObject category;
        fillPool.TryGetValue(parentID, out category);
        return category;
    }

    public static GameObject GetObject(GameObject objectReadyToPool)
    {//Se almacena el id del objeto para identificar su pool
        int id = objectReadyToPool.GetInstanceID();
        //en el caso de que el pool esté vacio, se crea el objeto
        if (pool[id].Count == 0)
        {
            CreateObject(objectReadyToPool);
        }
        //sacamos el objeto de la cola
        GameObject prefabCopy = pool[id].Dequeue();
        prefabCopy.SetActive(true);
        return prefabCopy;
    }



    public static void RecicleObject(GameObject objectReadyToPool, GameObject objectToRecicle)
    {
        //Se saca el id para saber a que piscina va
        int id = objectReadyToPool.GetInstanceID();
        //Se mete el objeto en la cola y lo desactivamos
        pool[id].Enqueue(objectToRecicle);
        objectToRecicle.SetActive(false);
    }



}
