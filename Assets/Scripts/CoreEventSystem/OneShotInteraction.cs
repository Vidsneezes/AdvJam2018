using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotInteraction : MonoBehaviour {

    public string uniqueId;
    public int desiredValueState;

    public ManagerBase managerBase;

    private void Start()
    {
        managerBase = GameObject.FindObjectOfType<ManagerBase>();
        if (managerBase != null)
        {
            int currentValue = managerBase.globalKeyItems.GetVariableState(uniqueId);
            if (currentValue == desiredValueState)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
	
    public void SetFireOff()
    {
        managerBase.globalKeyItems.SetGlobalVariable(uniqueId, desiredValueState, 0);
    }

    public bool HasBeenFired()
    {
        int currentValue = managerBase.globalKeyItems.GetVariableState(uniqueId);
        if (currentValue == desiredValueState)
        {
            return true;
        }
        return false;
    }

}
