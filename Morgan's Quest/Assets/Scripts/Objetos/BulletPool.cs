using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //Pool con nuestra cola 
    private ColaTF objectPool;
    private GameObject prefab; //prefab para usar en la pool
    private Transform poolContainer; //Lugar donde van a spawnear

    public BulletPool(GameObject prefab, int poolSize, Transform poolContainer = null)
    {
        this.prefab = prefab;
        this.poolContainer = poolContainer;

        objectPool = new ColaTF(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObject = CreateNewObject();
            ReturnToPool(newObject);
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = GameObject.Instantiate(prefab);
        if (poolContainer != null)
        {
            newObj.transform.SetParent(poolContainer);
        }
        newObj.SetActive(false);
        return newObj;
    }

    // Obtener un objeto de la cola
    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        if (objectPool.IsEmpty())
        {
            Debug.LogWarning("El pool está vacío, no hay objetos disponibles.");
            return null;
        }

        GameObject objectToUse = objectPool.Dequeue();

       
        objectToUse.transform.position = position;
        objectToUse.transform.rotation = rotation;
        objectToUse.SetActive(true);

        return objectToUse;
    }

    public void ReturnToPool(GameObject obj)
    {
        if (objectPool.IsFull())
        {
            Debug.LogWarning("El pool está lleno. No se puede retornar más objetos.");
            return;
        }

        // Desactivar el objeto y devolverlo al pool
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
