using System.Collections;
using System.Collections.Generic;
using System.Linq;

using HighlightPlus;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    public int objectsToFind = 10;
    int objectsFound;

    public GameObject textObject;
    
    public List<GameObject> objects;
    

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>(objectsToFind);

        for(int i = 0; i < objects.Capacity; i++)
        {
            GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("FindMe");
            GameObject randGameObject = gameobjects[Random.Range(0, gameobjects.Length)];
            if (!objects.Contains(randGameObject))
            {
                GameObject _textObject = Instantiate(textObject, GameObject.Find("ObjectList").transform);
                _textObject.name = randGameObject.name + "_" + randGameObject.transform.parent.name + "_text";
                TMPro.TextMeshProUGUI textComponent = _textObject.GetComponent<TMPro.TextMeshProUGUI>();

                textComponent.text = randGameObject.name + " (" + randGameObject.transform.parent.name + ")";

                LayoutElement layoutElement = _textObject.AddComponent<LayoutElement>();
                layoutElement.minHeight = 32;
                layoutElement.preferredHeight = 32;
                _textObject.transform.localScale = Vector3.one;
                objects.Add(randGameObject);
            }
        }

        addHighlighting(GameObject.Find("GameManager").GetComponent<GameManager>().highlightProfile);
    }

    public void addHighlighting(HighlightPlus.HighlightProfile profile)
    {
        for (int i = 0; i < this.objects.Count; i++)
        {
            GameObject obj = this.objects[i];

            HighlightEffect highlight = obj.AddComponent<HighlightEffect>();
            HighlightTrigger trigger = obj.AddComponent<HighlightTrigger>();
            if (!obj.GetComponent<ObjectProperty>()) obj.AddComponent<ObjectProperty>();

            highlight.ProfileLoad(profile);
        }
    }

    public bool find(GameObject gameObject)
    {
        return this.objects.Contains(gameObject);
    }

    public bool collectedAll()
    {
        return this.getCollectedObjects() >= this.objects.Count;
    }

    public int getCollectedObjects() {
        return objectsFound;
    }

    public void collect()
    {
        objectsFound++;
    }
}
