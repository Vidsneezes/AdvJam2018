using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorSystem : MonoBehaviour {

    public ActorResponseSheet actorResponseSheet;
    public UnityEvent onDialogueDone;
    [HideInInspector]
    public AudioSource effectsSounds;
    private int currentResponse;
    private int currentBranch;
    private bool hasFocus;

    private void Awake()
    {
        currentBranch = -1;
        hasFocus = false;
        currentResponse = -1;
      
    }

    private void Start()
    {
        GameObject audioPlayer = new GameObject("Effects");
        effectsSounds = audioPlayer.AddComponent<AudioSource>();
        effectsSounds.loop = false;
        effectsSounds.playOnAwake = false;
        effectsSounds.volume = 0.6f;
        effectsSounds.transform.SetParent(transform);
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

    public void RunDefaultDialogue(ManagerBase manageBase)
    {
        if(currentResponse == 0)
        {
            effectsSounds.clip = actorResponseSheet.talkEffect;
            effectsSounds.Play();
        }
        manageBase.dialogueBoxManager.pressITo.gameObject.SetActive(true);

        if (currentResponse >= actorResponseSheet.defaultResponses[currentBranch].dialogues.speechNode.Count)
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
            VirtualController.virtualController.onReTriggerable.RemoveListener(manageBase.dialogueBoxManager.ShowNextPrompt);

            return;
        }

        manageBase.dialogueBoxManager.OpenDialog(actorResponseSheet.defaultResponses[currentBranch].dialogues.speechNode[currentResponse].actorName, actorResponseSheet.defaultResponses[currentBranch].dialogues.speechNode[currentResponse].text);
        VirtualController.virtualController.inInteraction = true;
        currentResponse += 1;
        VirtualController.virtualController.onReTriggerable.AddListener(manageBase.dialogueBoxManager.ShowNextPrompt);
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
