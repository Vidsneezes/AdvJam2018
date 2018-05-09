using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KeyItemChangeEvent",menuName ="KeyItemChangeEvent")]
public class KeyItemChangeEvent : ScriptableObject {


    public List<KeyItemChangeContainer> keyItemContainers;
    public GlobalVariableList globalVariableList;

    public void RunEvent()
    {
        for (int i = 0; i < keyItemContainers.Count; i++)
        {
            globalVariableList.SetKeyItem(keyItemContainers[i].keyItemToChange, keyItemContainers[i].newState, keyItemContainers[i].newNumberData);
        }
    }
}

[System.Serializable]
public class KeyItemChangeContainer
{
    public string keyItemToChange;
    public int newState;
    public float newNumberData;
}
