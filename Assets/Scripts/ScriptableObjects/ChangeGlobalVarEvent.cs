using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KeyItemChangeEvent",menuName ="KeyItemChangeEvent")]
public class ChangeGlobalVarEvent : ScriptableObject {


    public List<GlobalVariableContainer> globalVarsToChange;
    public GlobalVariableList globalVariableList;

    public bool sceneTransport;
    public string sceneToTransport;

    public void RunEvent()
    {
        for (int i = 0; i < globalVarsToChange.Count; i++)
        {
            globalVariableList.SetGlobalVariable(globalVarsToChange[i].globalId, globalVarsToChange[i].state, globalVarsToChange[i].numberData);
        }

        if(sceneTransport)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToTransport);
        }
    }
}

