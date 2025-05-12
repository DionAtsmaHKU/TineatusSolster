using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Yarn.Unity;

public class VariableManager : MonoBehaviour
{
    public static VariableManager Instance { get; private set; }
    private InMemoryVariableStorage variables;
    [SerializeField] DialogueRunner runner;

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

        //runner.AddCommandHandler<string, float>("ChangeFloat", ChangeYarnFloat);
    }

    // Start is called before the first frame update
    void Start()
    {
        variables = FindObjectOfType<InMemoryVariableStorage>();
    }

    public void SetYarnFloat(string var, float value)
    {
        variables.SetValue(var, value);
    }

    public void SetYarnBool(string var, bool value)
    {
        variables.SetValue(var, value);
    }
    /*
    public void ChangeYarnFloat(string var, float value)
    {
        float oldValue = GetYarnFloat(var);
        float newValue = oldValue + value;
        variables.SetValue(var, newValue);
        Debug.Log("Set variable " + var + " from " + oldValue + " to " + newValue);
    }
    */
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
