using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour {

    public GameObject PooledObject;
    public int PooledAmount;
    public List<GameObject> ListofPooledObject;

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < PooledAmount; i++)
        {                                                                           //summary : created the value you put on pooledAmount and instantiate all and set them to false
            GameObject obj2 = (GameObject)Instantiate(PooledObject);     // list of amount of object instantiate
            obj2.SetActive(false);                                            // set the object in false
            ListofPooledObject.Add(obj2);                               //then put the object in the list of gameobject
        }
    }
    public GameObject getPooledObject()                                          // to access pooledobject which is inactive in the list
    {
        ListofPooledObject = new List<GameObject>();
        for (int i = 0; i < ListofPooledObject.Count; i++)
        {
            if (!ListofPooledObject[i].activeInHierarchy)            // active within the scene 
            {
                return ListofPooledObject[i];                            // return an active PoolObject
            }
        }
        GameObject obj = (GameObject)Instantiate(PooledObject);
        obj.SetActive(false);
        ListofPooledObject.Add(obj);
        return obj;
    }
}
