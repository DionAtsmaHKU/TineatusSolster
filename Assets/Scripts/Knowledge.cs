using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Knowledge : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] GameObject knowledgeMenu;
    [SerializeField] CustomObjectDictionary customObjectDictionary;
    private bool inMenu;
    private Dictionary<string, GameObject> knowledgeList;
    private Dictionary<string, bool> knowledgeBools = new Dictionary<string, bool>();

    private void Awake()
    {
        knowledgeList = customObjectDictionary.ToDictionary();
        foreach (var k in knowledgeList)
        {
            knowledgeBools.Add(k.Key, false);
        }
        dialogueRunner.AddCommandHandler<string>("Learn", Learn);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (inMenu)
            {
                knowledgeMenu.SetActive(true);
                inMenu = false;
            }
            else
            {
                knowledgeMenu.SetActive(false);
                inMenu = true;
            }
        }
    }

    private void Learn(string key)
    {
        knowledgeBools[key] = true;
        knowledgeList[key].SetActive(true);
    }

    public bool DoesHeKnow(string key)
    {
        if (knowledgeBools[key])
            return true;

        return false;
    }
}

[Serializable]
public class CustomObjectDictionary
{
    [SerializeField] List<CustomDictionaryObj> items;

    public Dictionary<string, GameObject> ToDictionary()
    {
        Dictionary<string, GameObject> newDict = new Dictionary<string, GameObject>();

        foreach (CustomDictionaryObj item in items)
        {
            newDict.Add(item.name, item.obj);
        }
        return newDict;
    }
}

[Serializable]
public class CustomDictionaryObj
{
    [SerializeField] public string name;
    [SerializeField] public GameObject obj;
}

