using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSystem : MonoBehaviour {

    public string actorName;
    public List<string> actorResponse;
    private int currentResponse;

    private void Awake()
    {
        currentResponse = 0;
    }

    public void RunDialogue()
    {
        ManagerBase manageBase = GameObject.FindObjectOfType<ManagerBase>();

        if (currentResponse >= actorResponse.Count)
        {
            VirtualController.virtualController.inInteraction = false;
            manageBase.dialogueBoxManager.CloseDialog();
            currentResponse = 0;
            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorName, actorResponse[currentResponse]);
        VirtualController.virtualController.inInteraction = true;
       
        currentResponse += 1;
        VirtualController.virtualController.BeginInteraction();
    }

}
