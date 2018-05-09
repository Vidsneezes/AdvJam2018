using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        bool hasItem = false;
        int itemId = -1;
        ///TODO add checks to only run if player is holding the item
        for (int i = 0; i < actorResponseSheet.actorResponses.Count; i++)
        {
            if(manageBase.globalKeyItems.HasItem(actorResponseSheet.actorResponses[i].keyItemName))
            {
                itemId = i;
                hasItem = true;
                break;
            }
        }
       
        if(hasItem)
        {
            RunResponseDialogue(itemId, manageBase);
        }
        else
        {
            RunDefaultDialogue(manageBase);
        }
    }

    public void RunResponseDialogue(int responseId, ManagerBase manageBase)
    {
        if (currentResponse >= actorResponseSheet.actorResponses[responseId].response.Count)
        {
            VirtualController.virtualController.inInteraction = false;
            manageBase.dialogueBoxManager.CloseDialog();
            currentResponse = 0;
            if(!actorResponseSheet.actorResponses[responseId].callEvent.Equals("nothing"))
            {
                manageBase.globalKeyItems.RunChangeEvent(actorResponseSheet.actorResponses[responseId].callEvent);
            }

            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorResponseSheet.actorName, actorResponseSheet.actorResponses[responseId].response[currentResponse]);
        VirtualController.virtualController.inInteraction = true;

        currentResponse += 1;
        VirtualController.virtualController.TriggerInteraction();
    }

    public void RunDefaultDialogue(ManagerBase manageBase)
    {
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
