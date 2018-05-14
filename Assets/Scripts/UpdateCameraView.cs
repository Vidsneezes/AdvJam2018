using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCameraView : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.orthographicSize = 3.5f;
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 3.5f, -10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
