using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject poolObject;
    [SerializeField] private int startingAmount;
    private List<GameObject> objectList;

    void Start()
    {
        //Create initial pool
        for (int i = 0; i < startingAmount; i++) 
        {
            CreateObject(poolObject);
        }
    }

    private GameObject CreateObject(GameObject poolObject)
    {
        //Create object append to list
        GameObject newObject = Instantiate(poolObject);
        objectList.Add( newObject );
        return newObject;
    }

    int objectPointer = 0; //Holds position between GetObject calls to avoid incrementing through whole list
    public GameObject GetObject()
    {
        for (int i=0; i<objectList.Count; i++)
        {
            //If object is inactive and waiting in pool
            if (!objectList[objectPointer].activeInHierarchy)
            {
                return objectList[objectPointer++]; //increment for next call
            }

            objectPointer++; //increment for next loop iteration
            if (objectPointer >= objectList.Count) //if spilling over pool, restart
            {
                objectPointer = 0;
            }
        }
        // After iterating through whole pool, if no objects available create object
        return CreateObject(poolObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
