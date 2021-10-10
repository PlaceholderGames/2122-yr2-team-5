using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public int objectsToFind = 10;
    
    private List<GameObject> objects;

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>(objectsToFind);

        for(int i = 0; i < objects.Capacity; i++)
        {
            GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("FindMe");
            GameObject randGameObject = gameobjects[Random.Range(0, gameobjects.Length)];
            objects.Add(randGameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> GetObjects()
    {
        return this.objects;
    }

    public void RemoveObject(GameObject obj)
    {
        if(this.objects.Contains(obj))
        {
            this.objects.Remove(obj);
        }
    }
}
