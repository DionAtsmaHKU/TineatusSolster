using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Yarn.Unity;
using System;

public class VariableManager : MonoBehaviour
{
    public static VariableManager Instance { get; private set; }
    private InMemoryVariableStorage variables;
    [SerializeField] DialogueRunner runner;

    public static event Action onLoop;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        variables = FindObjectOfType<InMemoryVariableStorage>();
    }

    public void Loop()
    {
        onLoop?.Invoke();
    }

    public void SetYarnFloat(string var, float value)
    {
        variables.SetValue(var, value);
    }

    public void SetYarnBool(string var, bool value)
    {
        variables.SetValue(var, value);
    }

    public float GetYarnFloat(string var)
    {
        float result;
        variables.TryGetValue(var, out result);
        return result;
    }

    public bool GetYarnBool(string var)
    {
        bool result;
        variables.TryGetValue(var, out result);
        return result;
    }
}
