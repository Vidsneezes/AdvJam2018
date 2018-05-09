using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float horSpeed;
    public float verSpeed;

    private float horVel;
    private float verVel;

	// Use this for initialization
	void Start () {
        horVel = 0;
        verVel = 0;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        Vector3 targetPos = target.position;
        Vector3 currentPosition = transform.position;
        if (targetPos.x > currentPosition.x + 2.5 )
        {
            currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetPos.x + 2.5f, horSpeed * Time.deltaTime);
        }else if (targetPos.x < currentPosition.x - 2.5)
        {
            currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetPos.x - 2.5f, horSpeed * Time.deltaTime);
        }

        currentPosition.y = Mathf.SmoothDamp(currentPosition.y, targetPos.y, ref verVel, verVel, verVel, Time.deltaTime);

        if (currentPosition.x > 45)
        {
            currentPosition.x = 45;
        }else if(currentPosition.x < 15)
        {
            currentPosition.x = 15;
        }

        if(currentPosition.y < 5)
        {
            currentPosition.y = 5;
        }

        transform.position = currentPosition;
        
    }
}
