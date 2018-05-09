using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSystem : MonoBehaviour {

    public ActorResponseSheet actorResponseSheet;
    private int currentResponse;

    private void Awake()
    {
        currentResponse = 0;
    }

    public void RunDialogue()
    {
        ManagerBase manageBase = GameObject.FindObjectOfType<ManagerBase>();

        if (currentResponse >= actorResponseSheet.defaultResponse.response.Count)
        {
            VirtualController.virtualController.inInteraction = false;
            manageBase.dialogueBoxManager.CloseDialog();
            currentResponse = 0;
            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorResponseSheet.actorName, actorResponseSheet.defaultResponse.response[currentResponse]);
        VirtualController.virtualController.inInteraction = true;
       
        currentResponse += 1;
        VirtualController.virtualController.TriggerInteraction();
    }

}
