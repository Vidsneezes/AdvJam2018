using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMeta : MonoBehaviour {

    public float unitWidth;
    public float unitHeight;

    public int locationId;

    public SpriteRenderer mainDisplay;
    public Rect worldRect;
    public PlayerController playerController;
    private string currentScene;

    public GlobalVariableList globals;

    private void Awake()
    {
        worldRect = new Rect(transform.position.x, transform.position.y, unitWidth, unitHeight);
    }

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        CameraFollow cameraFollow = GameObject.FindObjectOfType<CameraFollow>();

        if(playerController.inRoomTransition != 0)
        {
            Vector3 newPlayrPos = new Vector3(0, playerController.transform.position.y, playerController.transform.position.z);
            if(playerController.inRoomTransition == -1)
            {
                newPlayrPos.x = worldRect.x + worldRect.width - 1.5f;

            }else if(playerController.inRoomTransition == 1)
            {
                newPlayrPos.x = worldRect.x + 1.5f;
            }

            playerController.transform.position = newPlayrPos;
            playerController.inRoomTransition = 0;
            newPlayrPos.z = -10;
            newPlayrPos.y = 8;
            if (playerController.inRoomTransition == -1)
            {
                newPlayrPos.x = worldRect.x + worldRect.width + cameraFollow.cameraBounds.x;

            }
            else if (playerController.inRoomTransition == 1)
            {
                newPlayrPos.x = worldRect.x + 0.5f - cameraFollow.cameraBounds.x;
            }

            cameraFollow.transform.position = newPlayrPos;
        }
        mainDisplay.sortingOrder = -500;
#if !UNITY_EDITOR
        mainDisplay.gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
      
        int nextLocation = 1;


        if (playerController.transform.position.x < worldRect.x + 0.5f)
        {
            int currentAct = globals.GetVariableState("act");
            playerController.inRoomTransition = -1;
           
            if (locationId == 3)
            {
                nextLocation = 2;
            }else if(locationId == 2)
            {
                nextLocation = 1;
            }else if(locationId == 1)
            {
                nextLocation = 3;
            }

            string nextScene = string.Format("location_{0}_act_{1}", nextLocation, currentAct);

            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);

        }else if(playerController.transform.position.x > worldRect.x + worldRect.width - 0.5f)
        {
            int currentAct = globals.GetVariableState("act");

            playerController.inRoomTransition = 1;

            if (locationId == 3)
            {
                nextLocation = 1;
            }
            else if (locationId == 2)
            {
                nextLocation = 3;
            }
            else if (locationId == 1)
            {
                nextLocation = 2;
            }

            string nextScene = string.Format("location_{0}_act_{1}", nextLocation, currentAct);

            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }

}
