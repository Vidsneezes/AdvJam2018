using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractBase : MonoBehaviour, Interactable {

    public UnityEvent onInteractEnter;
    public UnityEvent onInteractActived;
    public UnityEvent onInteractExit;
    public bool OneShot;

    private bool hasFired;

    void Awake()
    {
        hasFired = false;
    }

    public void OnInteractionEnter()
    {
        onInteractEnter.Invoke();
    }

    public void OnInteractionExit()
    {
        onInteractExit.Invoke();
    }

    public void OnInteractionActivated()
    {
        onInteractActived.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnInteractionEnter();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (VirtualController.virtualController.wasInteractionPressed)
        {
            if (!OneShot || (OneShot && !hasFired))
            {
                OnInteractionActivated();
                hasFired = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnInteractionExit();
    }

}
