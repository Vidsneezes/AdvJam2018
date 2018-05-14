using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float horSpeed;
    public float verSpeed;

    public float smoothDamp;

    public LocationMeta locationMeta;
    
    private float horVel;
    private float verVel;

    private Camera myCamera;

    public Vector2 cameraBounds
    {
        get
        {
            float height = 2f * myCamera.orthographicSize;
            float width = height * myCamera.aspect;
            return new Vector2(width, height);
        }
    }

    private Vector3 vel;

	// Use this for initialization
	void Start () {
        vel = new Vector3();
        locationMeta = GameObject.FindObjectOfType<LocationMeta>();
        myCamera = GetComponent<Camera>();
        horVel = 0;
        verVel = 0;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        if(target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if(playerObject != null)
            {
                target = playerObject.transform;
                transform.position = target.position + Vector3.forward * -10;
            }
        }

        if(locationMeta == null)
        {
            locationMeta = GameObject.FindObjectOfType<LocationMeta>();
        }

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

        if (currentPosition.x - cameraBounds.x * 0.5f < locationMeta.worldRect.x )
        {
            currentPosition.x = locationMeta.worldRect.x + cameraBounds.x * 0.5f;
        }
        if(currentPosition.x + cameraBounds.x * 0.5f > locationMeta.worldRect.x + locationMeta.worldRect.width)
        {
            currentPosition.x = locationMeta.worldRect.x + locationMeta.worldRect.width - cameraBounds.x * 0.5f;
        }

        if(currentPosition.y < myCamera.orthographicSize)
        {
            currentPosition.y = myCamera.orthographicSize;
        }

        transform.position = Vector3.SmoothDamp(transform.position, currentPosition, ref vel, smoothDamp);
        
    }
}
