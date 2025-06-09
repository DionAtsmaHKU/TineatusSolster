using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SetModel : MonoBehaviour
{
    [SerializeField] DialogueRunner runner;
    [SerializeField] GameObject artModel;
    [SerializeField] GameObject techModel;
    [SerializeField] GameObject psychModel;

    private void Awake()
    {
        runner.AddCommandHandler<string>("SetModel", SetSashaModel);
    }

    private void SetSashaModel(string model)
    {
        if (model == "Art")
        {
            artModel.SetActive(true);
            techModel.SetActive(false);
            psychModel.SetActive(false);
        }
        if (model == "Tech")
        {
            techModel.SetActive(true);
            artModel.SetActive(false);
            psychModel.SetActive(false);
        }
        if (model == "Psych")
        {
            psychModel.SetActive(true);
            artModel.SetActive(false);
            techModel.SetActive(false);
        }
    }
}
