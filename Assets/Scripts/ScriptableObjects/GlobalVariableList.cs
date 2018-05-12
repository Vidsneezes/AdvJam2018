using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Global Variables")]
public class GlobalVariableList : ScriptableObject {
    public List<GlobalVariableContainer> variables;

    public List<KeyItemChangeEvent> keyItemChangeEvents;

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

    public void RunChangeEvent(string kiceName)
    {
        for (int i = 0; i < keyItemChangeEvents.Count; i++)
        {
            if(keyItemChangeEvents[i].name.Equals(kiceName))
            {
                keyItemChangeEvents[i].RunEvent();
                return;
            }
        }
    }

    public void SetKeyItem(string globalItemKey, int newState, float newNumberData)
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

    public void PickUpKeyItem(string globalItemKey)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (variables[i].globalId.Equals(globalItemKey))
            {
                variables[i].state = 1;
                return;
            }
        }
    }

    public void UseUpItem(string globalItemKey)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (variables[i].globalId.Equals(globalItemKey))
            {
                variables[i].state = 2;
            }
        }
    }

    public bool HasItem(string globalItemKey)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (variables[i].globalId.Equals(globalItemKey))
            {
                return variables[i].state == 1;
            }
        }
        return false;
    }

    public bool ItemInWorld(string globalItemKey)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (variables[i].globalId.Equals(globalItemKey))
            {
                return variables[i].state == 0;
            }
        }
        return false;
    }
}

[System.Serializable]
public class GlobalVariableContainer
{
    public string globalId;
    public int state;
    public float numberData;
}
