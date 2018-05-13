using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {

    public OneShotIDb oneShotData;
    public PlayerSpawner ps_prefab;

	// Use this for initialization
	void Start () {
        //Reset all data here or load previous data if exists
        for (int i = 0; i < oneShotData.oneShots.Count; i++)
        {
            if (oneShotData.oneShots[i].uniqueId.Equals("playerspawnpoint"))
            {
                oneShotData.oneShots[i].fired = false;
            }
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("CoreScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("location_2_act_1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

}
