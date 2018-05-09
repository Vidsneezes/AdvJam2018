using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="OneShotIDb", menuName ="Globals/OneShotIDb")]
public class OneShotIDb : ScriptableObject {

    public List<OneShotContainer> oneShots;

    public void FireOff(string uniqueId)
    {
        for (int i = 0; i < oneShots.Count; i++)
        {
            if (oneShots[i].uniqueId.Equals(uniqueId))
            {
                oneShots[i].fired = true;
                return;
            }
        }

        OneShotContainer oneSh = new OneShotContainer();
        oneSh.fired = true;
        oneSh.uniqueId = uniqueId;
        oneShots.Add(oneSh);
    }

    public bool HasBeenFired(string uniqueId)
    {
        for (int i = 0; i < oneShots.Count; i++)
        {
            if(oneShots[i].uniqueId.Equals(uniqueId))
            {
                return oneShots[i].fired;
            }
        }
        return false;
    }

}

[System.Serializable]
public class OneShotContainer
{
    public string uniqueId;
    public bool fired;
}
