using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ActorResponseSheet",menuName ="Dialogue/ActorResponseSheet")]
public class ActorResponseSheet : ScriptableObject {

    public List<ActorResponseContainer> defaultResponses;
}

[System.Serializable]
public class ActorResponseContainer
{
    public DialogueData dialogues;
    public List<GlobalVariableContainer> globalConditions;
    public ChangeGlobalVarEvent callEvent;
}