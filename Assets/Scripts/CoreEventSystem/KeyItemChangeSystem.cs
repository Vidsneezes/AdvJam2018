using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemChangeSystem : MonoBehaviour {

    public KeyItemChangeEvent keyItemChangeEvent;

    public void RunEvent()
    {
        keyItemChangeEvent.RunEvent();
    }
}
