using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLocation : MonoBehaviour {

    public string newLocation;
    public Vector3 newPosition;
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RunEvent()
    {
        PlayerController pc = GameObject.FindObjectOfType<PlayerController>();
        pc.transform.position = newPosition;
        UnityEngine.SceneManagement.SceneManager.LoadScene(newLocation);
    }

}
