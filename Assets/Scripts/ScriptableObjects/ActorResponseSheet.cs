using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ActorResponseSheet",menuName ="Dialogue/ActorResponseSheet")]
public class ActorResponseSheet : ScriptableObject {

    public string actorName; // replace this with an actor profile
    public List<ActorResponseContainer> actorResponses;

    public ActorResponseContainer defaultResponse;


}

[System.Serializable]
public class ActorResponseContainer
{
    public string keyItemName;
    public List<string> response;
    public string callEvent;
}