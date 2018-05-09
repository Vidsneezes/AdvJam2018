using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractBase : MonoBehaviour, Interactable {

    public UnityEvent onInteractEnter;
    public UnityEvent onInteractActived;
    public UnityEvent onInteractExit;

    void Awake()
    {
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
        OnInteractionActivated();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnInteractionExit();
    }

}
