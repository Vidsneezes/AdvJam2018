using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemInteract : MonoBehaviour {

    public string keyItem;

    private ManagerBase mbase;

    private void Start()
    {
        mbase = GameObject.FindObjectOfType<ManagerBase>();
        if(!mbase.globalKeyItems.ItemInWorld(keyItem))
        {
            GameObject.Destroy(gameObject);
        }
    }


    public void Run()
    {
        mbase.globalKeyItems.PickUpKeyItem(keyItem);
        GameObject.Destroy(gameObject);
    }


}
