using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Global Variables")]
public class GlobalVariableList : ScriptableObject {
    public List<GlobalVariableContainer> variables;

    public List<GlobalVariableContainer> dynamicList;

    public int GetVariableState(string keyName)
    {
        for (int i = 0; i < dynamicList.Count; i++)
        {
            if (dynamicList[i].globalId.Equals(keyName))
            {
                return dynamicList[i].state;
            }
        }

        return 1;
    }

    public void SetGlobalVariable(string globalItemKey, int newState, float newNumberData)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (dynamicList[i].globalId.Equals(globalItemKey))
            {
                dynamicList[i].state = newState;
                dynamicList[i].numberData = newNumberData;
                return;

            }
        }
    }

    public void BuildDynamicList()
    {
        dynamicList = new List<GlobalVariableContainer>(variables.ToArray());
    }

}

[System.Serializable]
public class GlobalVariableContainer
{
    public string globalId;
    public int state;
    public float numberData;
}
