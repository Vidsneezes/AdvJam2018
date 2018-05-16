using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialogue",menuName ="Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject {

    public List<SpeechNode> speechNode;

}

[System.Serializable]
public class SpeechNode
{
    public string actorName;
    public string text;
}
