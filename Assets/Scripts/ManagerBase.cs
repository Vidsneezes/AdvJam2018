using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase : MonoBehaviour {

    public DialogueBoxManager dialogueBoxManager;
    public GlobalVariableList globalKeyItems;
    public int currentItem = -1;

    public bool ConditionMeets(string globalId, int desiredState)
    {
        return desiredState == globalKeyItems.GetVariableState(globalId);
    }

}
