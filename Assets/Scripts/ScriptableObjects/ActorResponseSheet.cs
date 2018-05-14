using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ActorResponseSheet",menuName ="Dialogue/ActorResponseSheet")]
public class ActorResponseSheet : ScriptableObject {

    public string actorName; // replace this with an actor profile
    public List<ActorResponseContainer> actorItemResponses;


    public List<ActorResponseContainer> defaultResponses;


}

[System.Serializable]
public class ActorResponseContainer
{
    public List<string> response;
    public List<GlobalVariableContainer> globalConditions;
    public ChangeGlobalVarEvent callEvent;
}