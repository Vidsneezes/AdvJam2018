using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public PlayerController playerPrefab;

    private void Awake()
    {
        OneShotInteraction oneShotI = GetComponent<OneShotInteraction>();

        if (!oneShotI.HasBeenFired())
        {
            PlayerController pc = GameObject.Instantiate(playerPrefab, transform.position, Quaternion.identity);
            DontDestroyOnLoad(pc);
            GameObject.Destroy(gameObject);
        }
        oneShotI.SetFireOff();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
