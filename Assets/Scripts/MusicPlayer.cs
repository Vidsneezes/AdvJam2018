using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioClip jamsFull;
    public AudioClip spooky;
    public AudioClip act_1;

    public AudioSource musicPlayer;
    public GlobalVariableList globalVaraibleList;

    public int lastAct;

    private bool inTransition;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        musicPlayer.volume = 0.65f;
    }

    // Use this for initialization
    void Start () {
        inTransition = false;
        int currentAct = globalVaraibleList.GetVariableState("act");
        lastAct = 1;
        musicPlayer.loop = true;
        musicPlayer.clip = act_1;
        musicPlayer.Play();
    }

    // Update is called once per frame
    void Update () {
        lastAct = globalVaraibleList.GetVariableState("act");
        switch (lastAct)
        {
            case 1: PlayFirstAct();break;
            case 2: PlaySecondAct();break;
            case 3: PlaySecondAct();break;
            case 4: PlayFinal();break;
        }
	}

    public void PlayFirstAct()
    {
        if (musicPlayer.isPlaying && musicPlayer.clip != act_1)
        {
            musicPlayer.Stop();
            musicPlayer.clip = act_1;
            musicPlayer.Play();
        }
    }

    public void PlaySecondAct()
    {
        if(musicPlayer.isPlaying && musicPlayer.clip != spooky)
        {
            musicPlayer.Stop();
            musicPlayer.clip = spooky;
            musicPlayer.Play();
        }
      
    }

    public void PlayFinal()
    {
        if (musicPlayer.isPlaying && musicPlayer.clip != jamsFull)
        {
            musicPlayer.Stop();
            musicPlayer.clip = jamsFull;
            musicPlayer.Play();
        }
    }

}
