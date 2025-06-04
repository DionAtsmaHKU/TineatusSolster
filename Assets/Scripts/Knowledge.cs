using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class Knowledge : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] GameObject knowledgeMenu;
    [SerializeField] CustomObjectDictionary knowledgeDict;
    private bool inMenu;
    private Dictionary<string, GameObject> knowledgeList;
    private Dictionary<string, bool> knowledgeBools = new Dictionary<string, bool>();
    private Func<string, bool> DoesHeKnowFunc;

    // Sound Stuff
    private string newInfoPath = "event:/ui/new_info_noted";
    private EventInstance newInfoEvent;

    private void Awake()
    {
        newInfoEvent = RuntimeManager.CreateInstance(newInfoPath);

        DoesHeKnowFunc += DoesHeKnow;
        knowledgeList = knowledgeDict.ToDictionary();
        foreach (var k in knowledgeList)
        {
            knowledgeBools.Add(k.Key, false);
        }
        dialogueRunner.AddCommandHandler<string>("Learn", Learn);
        dialogueRunner.AddFunction<string, bool>("DoesHeKnow", DoesHeKnowFunc);
        VariableManager.onLoop += LearnLoop;
    }

    private void OnDestroy()
    {
        VariableManager.onLoop -= LearnLoop;
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

    private void LearnLoop()
    {
        Debug.Log("SecondLoop learned");
        Learn("SecondLoop");
    }

    private void Learn(string key)
    {
        Debug.Log("LEARN" + key);
        if (knowledgeBools.ContainsKey(key) && knowledgeBools[key] == true)
            return;

        Debug.Log("New info event");
        newInfoEvent.start();
        knowledgeBools[key] = true;

        if (knowledgeList.ContainsKey(key))
        {
            knowledgeList[key].SetActive(true);
        }
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

