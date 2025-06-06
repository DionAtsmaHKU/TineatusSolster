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
    private bool inMenu;
    private Dictionary<string, GameObject> knowledgeDict = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> knowledgeBools = new Dictionary<string, bool>();
    private Func<string, bool> DoesHeKnowFunc;

    // Sound Stuff
    private string newInfoPath = "event:/ui/new_info_noted";
    private EventInstance newInfoEvent;

    private void Awake()
    {
        newInfoEvent = RuntimeManager.CreateInstance(newInfoPath);

        DoesHeKnowFunc += DoesHeKnow;
        knowledgeDict.Add("SecondLoop", null);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Knowledge"))
        {
            knowledgeDict.Add(obj.name, obj);
            Debug.Log("Adding " + obj.name);
            obj.SetActive(false);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Deactivate"))
        {
            obj.SetActive(false);
        }

        foreach (var k in knowledgeDict)
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

        if (knowledgeDict[key] != null)
        {
            knowledgeDict[key].SetActive(true);
        }
    }

    public bool DoesHeKnow(string key)
    {
        if (knowledgeBools[key])
            return true;

        return false;
    }
}
