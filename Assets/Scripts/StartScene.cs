using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {

    public GlobalVariableList globalsList;
    public PlayerSpawner ps_prefab;

	// Use this for initialization
	void Start () {
        //Reset all data here or load previous data if exists
        globalsList.SetGlobalVariable("playerspawnpoint", 0, 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("CoreScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("location_2_act_1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

}
