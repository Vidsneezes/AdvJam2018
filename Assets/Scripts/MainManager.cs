using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("CoreScene");

    }

}
