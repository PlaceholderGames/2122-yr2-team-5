using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public int objectsToFind = 10;
    int objectsFound;
    
    private List<GameObject> objects;

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>(objectsToFind);

        for(int i = 0; i < objects.Capacity; i++)
        {
            GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("FindMe");
            GameObject randGameObject = gameobjects[Random.Range(0, gameobjects.Length)];
            if(!objects.Contains(randGameObject)) objects.Add(randGameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool collectedAll()
    {
        return this.getCollectedObjects() >= this.objects.Count;
    }

    public int getCollectedObjects() {
        return objectsFound;
    }

    public void collect(GameObject go)
    {
        objectsFound++;
    }
}
