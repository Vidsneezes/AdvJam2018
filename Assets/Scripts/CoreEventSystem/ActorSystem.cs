using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorSystem : MonoBehaviour {

    public ActorResponseSheet actorResponseSheet;
    public UnityEvent onDialogueDone;
    private int currentResponse;
    private int currentBranch;
    private bool hasFocus;

    private void Awake()
    {
        currentBranch = -1;
        hasFocus = false;
        currentResponse = -1;
    }

    public void DecideDialogue()
    {
        ManagerBase manageBase = GameObject.FindObjectOfType<ManagerBase>();
        

        if(manageBase.currentItem == -1)
        {
            if(currentBranch == -1)
            {
                for (int i = 0; i < actorResponseSheet.defaultResponses.Count; i++)
                {
                    if(MeetsConditions(manageBase,actorResponseSheet.defaultResponses[i]))
                    {
                        currentBranch = i;
                        break;
                    }
                }
            }

            RunDefaultDialogue(manageBase);
        }
        else
        {
            RunResponseDialogue(manageBase.currentItem, manageBase);
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
        if (currentResponse >= actorResponseSheet.actorItemResponses[responseId].response.Count)
        {
            hasFocus = false;

            VirtualController.virtualController.inInteraction = false;
            manageBase.dialogueBoxManager.CloseDialog();
            currentResponse = 0;
            Debug.Log("close box");
            if(actorResponseSheet.actorItemResponses[responseId].callEvent != null)
            {
                actorResponseSheet.actorItemResponses[responseId].callEvent.RunEvent();
            }
            onDialogueDone.Invoke();
            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorResponseSheet.actorName, actorResponseSheet.actorItemResponses[responseId].response[currentResponse]);
        VirtualController.virtualController.inInteraction = true;

        currentResponse += 1;
        VirtualController.virtualController.TriggerInteraction();
        hasFocus = true;
    }

    public void RunDefaultDialogue(ManagerBase manageBase)
    {
        if (currentResponse >= actorResponseSheet.defaultResponses[currentBranch].response.Count)
        {
            hasFocus = false;
            VirtualController.virtualController.inInteraction = false;
            manageBase.dialogueBoxManager.CloseDialog();
            currentResponse = 0;
            onDialogueDone.Invoke();
            if(actorResponseSheet.defaultResponses[currentBranch].callEvent != null)
            {
                actorResponseSheet.defaultResponses[currentBranch].callEvent.RunEvent();
            }
            Debug.Log("close box");
            currentBranch = -1;
            currentResponse = -1;
            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorResponseSheet.actorName, actorResponseSheet.defaultResponses[currentBranch].response[currentResponse]);
        VirtualController.virtualController.inInteraction = true;

        currentResponse += 1;
        VirtualController.virtualController.TriggerInteraction();
        hasFocus = true;
    }

    public bool MeetsConditions(ManagerBase managerBase,ActorResponseContainer actorResponseContainer)
    {
        if(actorResponseContainer.globalConditions.Count == 0)
        {
            return true;
        }

        for (int i = 0; i < actorResponseContainer.globalConditions.Count; i++)
        {
            int currentValue = managerBase.globalKeyItems.GetVariableState(actorResponseContainer.globalConditions[i].globalId);
            int desiredValue = actorResponseContainer.globalConditions[i].state;

            if(currentValue != desiredValue)
            {
                return false;
            }
        }

        return true;
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
