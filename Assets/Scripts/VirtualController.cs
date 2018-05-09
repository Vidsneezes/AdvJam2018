using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualController : MonoBehaviour {

    private static VirtualController v_c;

    public static VirtualController virtualController
    {
        get
        {
            if(v_c != null)
            {
                return v_c;
            }
            return null;
        }
    }

    public bool isRightPressed;
    public bool isLeftPressed;
    public bool wasSpacePressed;
    public bool wasInteractionPressed;

    public bool inInteraction;
    public float interactionRecharge;

    private void Awake()
    {
        VirtualController.v_c = this;
    }

    private void OnDestroy()
    {
        VirtualController.v_c = null;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (interactionRecharge > 0.5f)
        {
            interactionRecharge = 0.6f;
            wasInteractionPressed = Input.GetKeyDown(KeyCode.I);
        }
        else
        {
            interactionRecharge += Time.deltaTime;
        }
        if (!inInteraction)
        {
            isRightPressed = Input.GetKey(KeyCode.D);
            isLeftPressed = Input.GetKey(KeyCode.A);
            wasSpacePressed = Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            isRightPressed = false;
            isLeftPressed = false;
            wasSpacePressed = false;
        }


    }

    public void BeginInteraction()
    {
        interactionRecharge = 0;
    }
}
