using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractBase : MonoBehaviour, Interactable {

    public bool hasCondition;
    public string globalId;
    public int desiredState;

    public UnityEvent onInteractEnter;
    public UnityEvent onInteractActived;
    public UnityEvent onInteractExit;

    private ManagerBase managerBase;
    void Awake()
    {
    }

    void Start()
    {
        managerBase = GameObject.FindObjectOfType<ManagerBase>();
    }

    public void OnInteractionEnter()
    {
        if (managerBase.playerController != null)
        {
            managerBase.playerController.interactionIcon.gameObject.SetActive(true);
        }

        if (!hasCondition)
        {
            onInteractEnter.Invoke();

            return;
        }

        if (hasCondition && managerBase.ConditionMeets(globalId, desiredState))
        {
            onInteractEnter.Invoke();
        }

        
    }

    public void OnInteractionExit()
    {
        if (managerBase.playerController != null)
        {
            managerBase.playerController.interactionIcon.gameObject.SetActive(false);
        }

        if (!hasCondition)
        {
            onInteractExit.Invoke();

            return;
        }
        if (hasCondition && managerBase.ConditionMeets(globalId, desiredState))
        {
            onInteractExit.Invoke();
        }
    }

    public void OnInteractionActivated()
    {
        if (!hasCondition)
        {
            onInteractActived.Invoke();

            return;
        }
        if (hasCondition && managerBase.ConditionMeets(globalId, desiredState))
        {
            if (managerBase.playerController != null)
            {
                managerBase.playerController.interactionIcon.gameObject.SetActive(true);
            }

            onInteractActived.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnInteractionEnter();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnInteractionActivated();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnInteractionExit();
    }

}
