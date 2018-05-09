using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorSystem : MonoBehaviour {

    public ActorResponseSheet actorResponseSheet;
    public UnityEvent onDialogueDone;
    private int currentResponse;
    private bool hasFocus;

    private void Awake()
    {
        hasFocus = false;
        currentResponse = 0;
    }

    public void DecideDialogue()
    {
        ManagerBase manageBase = GameObject.FindObjectOfType<ManagerBase>();
        bool hasItem = false;
        int itemId = -1;

        ///TODO add checks to only run if player is holding the item
        for (int i = 0; i < actorResponseSheet.actorResponses.Count; i++)
        {
            if (manageBase.globalKeyItems.HasItem(actorResponseSheet.actorResponses[i].keyItemName))
            {
                itemId = i;
                hasItem = true;
                break;
            }
        }

        if (hasItem)
        {
            RunResponseDialogue(itemId, manageBase);
        }
        else
        {
            RunDefaultDialogue(manageBase);
        }
    }

    public void RunDialogue()
    {
        Debug.Log(name + "Has Focus");
        currentResponse = 0;
        hasFocus = true;
    }

    public void OnLeave()
    {
        Debug.Log(name + "lost focus");

        hasFocus = false;
    }

    public void RunResponseDialogue(int responseId, ManagerBase manageBase)
    {
        if (currentResponse >= actorResponseSheet.actorResponses[responseId].response.Count)
        {
            hasFocus = false;

            VirtualController.virtualController.inInteraction = false;
            manageBase.dialogueBoxManager.CloseDialog();
            currentResponse = 0;
            Debug.Log("close box");
            if(!actorResponseSheet.actorResponses[responseId].callEvent.Equals("nothing"))
            {
                manageBase.globalKeyItems.RunChangeEvent(actorResponseSheet.actorResponses[responseId].callEvent);
            }
            onDialogueDone.Invoke();
            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorResponseSheet.actorName, actorResponseSheet.actorResponses[responseId].response[currentResponse]);
        VirtualController.virtualController.inInteraction = true;

        currentResponse += 1;
        VirtualController.virtualController.TriggerInteraction();
        hasFocus = true;
    }

    public void RunDefaultDialogue(ManagerBase manageBase)
    {
        if (currentResponse >= actorResponseSheet.defaultResponse.response.Count)
        {
            hasFocus = false;
            VirtualController.virtualController.inInteraction = false;
            manageBase.dialogueBoxManager.CloseDialog();
            currentResponse = 0;
            onDialogueDone.Invoke();
            manageBase.globalKeyItems.RunChangeEvent(actorResponseSheet.defaultResponse.callEvent);
            Debug.Log("close box");

            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorResponseSheet.actorName, actorResponseSheet.defaultResponse.response[currentResponse]);
        VirtualController.virtualController.inInteraction = true;

        currentResponse += 1;
        VirtualController.virtualController.TriggerInteraction();
        hasFocus = true;
    }


    public void Update()
    {
        if (hasFocus)
        {
            if (VirtualController.virtualController.wasInteractionPressed)
            {
                DecideDialogue();
            }
        }
    }
}
