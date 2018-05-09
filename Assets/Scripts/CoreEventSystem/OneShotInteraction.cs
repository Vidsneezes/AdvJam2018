using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotInteraction : MonoBehaviour {

    public string uniqueId;

    public ManagerBase managerBase;

    private void Start()
    {
        managerBase = GameObject.FindObjectOfType<ManagerBase>();
        if(managerBase.oneShotIDb.HasBeenFired(uniqueId))
        {
            GameObject.Destroy(gameObject);
        }
    }
	
    public void SetFireOff()
    {
        managerBase.oneShotIDb.FireOff(uniqueId);
    }

    public bool HasBeenFired()
    {
        if (managerBase.oneShotIDb.HasBeenFired(uniqueId))
        {
            return true;
        }
        return false;
    }

}
