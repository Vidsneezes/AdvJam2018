using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour {

    private bool canExit = false;
	// Use this for initialization
	void Start () {
        MainManager mainManager = GameObject.FindObjectOfType<MainManager>();
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        if(mainManager != null)
        {
            Destroy(mainManager.gameObject);
        }

        if(playerController != null)
        {
            Destroy(playerController.gameObject);
        }
        AudioSource mm = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
        mm.volume = 0.9f;
        Invoke("MayExit", 4f);
	}

    public void MayExit()
    {
        canExit = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(canExit && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
