using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Global Variables")]
public class GlobalVariableList : ScriptableObject {
    public List<GlobalVariableContainer> variables;

    public int GetVariableState(string keyName)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (variables[i].globalId.Equals(keyName))
            {
                return variables[i].state;
            }
        }

        return 1;
    }

    public void SetGlobalVariable(string globalItemKey, int newState, float newNumberData)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (variables[i].globalId.Equals(globalItemKey))
            {
                variables[i].state = newState;
                variables[i].numberData = newNumberData;
                return;

            }
        }
    }

}

[System.Serializable]
public class GlobalVariableContainer
{
    public string globalId;
    public int state;
    public float numberData;
}
