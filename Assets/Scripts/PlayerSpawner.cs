using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public PlayerController playerPrefab;

    private void Awake()
    {
        PlayerController pc = GameObject.Instantiate(playerPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(pc);
        GameObject.Destroy(gameObject);
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
