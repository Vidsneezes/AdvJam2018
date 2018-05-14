using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HelperMenu : MonoBehaviour {
    
    static void RunFromCurrent()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }


        GlobalVariableList globalsList = (GlobalVariableList)AssetDatabase.LoadAssetAtPath("Assets/Data/GlobalVariables.asset", typeof(GlobalVariableList));
        globalsList.SetGlobalVariable("playerspawnpoint", 0, 0);
      
        string currentScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name;
        Debug.Log("load");
        if (currentScene != "CoreScene")
        {
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/CoreScene.unity");
            //UnityEditor.SceneManagement.EditorSceneManager.LoadScene(currentScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        AddPlayerSpawnPoint();

        UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Level Editor/Save Globals On To Scene")]
    public static void SaveGlobalsRef()
    {
        LocationMeta lm = GameObject.FindObjectOfType<LocationMeta>();
        if(lm != null)
        {
            lm.globals = (GlobalVariableList)AssetDatabase.LoadAssetAtPath("Assets/Data/GlobalVariables.asset", typeof(GlobalVariableList));
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
            UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
    }

    public static void OnPlayStateChange(PlayModeStateChange playMode)
    {
        if(playMode == PlayModeStateChange.EnteredEditMode)
        {
            // RemoveGameLogicObjects();
            UnityEditor.SceneManagement.EditorSceneManager.CloseScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("CoreScene"), true);
        }else if(playMode == PlayModeStateChange.ExitingEditMode)
        {
            string currentScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;
            if (!currentScene.Contains("StartScene"))
            {
                GlobalVariableList globalsList = (GlobalVariableList)AssetDatabase.LoadAssetAtPath("Assets/Data/GlobalVariables.asset", typeof(GlobalVariableList));
                globalsList.SetGlobalVariable("playerspawnpoint", 0, 0);

                if (currentScene != "CoreScene")
                {
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/CoreScene.unity");
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene(currentScene, UnityEditor.SceneManagement.OpenSceneMode.Additive);
                }
                EditorApplication.isPlaying = true;
                AddPlayerSpawnPoint();
            }
          //  AddGameLogicObjects();
        }
    }

    [MenuItem("Level Editor/Player/Add Player Spawn Point")]
    static void AddPlayerSpawnPoint()
    {
        PlayerSpawner[] ps = GameObject.FindObjectsOfType<PlayerSpawner>();
        if (ps.Length == 0)
        {
            PlayerSpawner ps_prefab = AssetDatabase.LoadAssetAtPath<PlayerSpawner>("Assets/Prefabs/LevelBuilding/PlayerSpawnPoint.prefab");
            PlayerSpawner ps_obj = GameObject.Instantiate(ps_prefab);
            ps_obj.name = "###PLAYERSPAWNPOINT###";
        }
    }

    [MenuItem("Level Editor/Player/Remove Player Spawn Point")]
    static void RemovePlayerSpawnPoint()
    {
        PlayerSpawner[] ps = GameObject.FindObjectsOfType<PlayerSpawner>();

        for (int i = 0; i < ps.Length; i++)
        {
            GameObject.DestroyImmediate(ps[i].gameObject);
        }
    }

    [MenuItem("Level Editor/Clean Scene")]
    static void RemoveGameLogicObjects()
    {
        PlayerSpawner[] ps = GameObject.FindObjectsOfType<PlayerSpawner>();

        for (int i = 0; i < ps.Length; i++)
        {
            GameObject.DestroyImmediate(ps[i].gameObject);
        }

        if (GameObject.FindGameObjectWithTag("PlayBase") != null)
        {
            GameObject mm = GameObject.FindGameObjectWithTag("PlayBase");
            GameObject.DestroyImmediate(mm);
            return;
        }
        else
        {
            VirtualController vc = GameObject.FindObjectOfType<VirtualController>();
            if (vc != null)
            {
                GameObject.DestroyImmediate(vc.gameObject);

            }
            ManagerBase mb = GameObject.FindObjectOfType<ManagerBase>();


            if (mb != null && mb.dialogueBoxManager != null)
            {
                GameObject.DestroyImmediate(mb.dialogueBoxManager.gameObject);
            }
            else
            {
                DialogueBoxManager dbm = GameObject.FindObjectOfType<DialogueBoxManager>();
                if (dbm != null)
                {
                    GameObject.DestroyImmediate(dbm.gameObject);
                }
            }

            GameObject.DestroyImmediate(mb.gameObject);
        }

       
    }

    static void AddGameLogicObjects()
    {
        GameObject playableBase = new GameObject("###PlayableBase###");
        playableBase.tag = "PlayBase";
        playableBase.AddComponent<MainManager>();
        HierarchyHighlighterComponent hhc = playableBase.AddComponent<HierarchyHighlighterComponent>();
        hhc.color = Color.red;
        hhc.highlight = true;

        playableBase.transform.position = new Vector3(0, 0, 0);
        VirtualController vc = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<VirtualController>("Assets/Prefabs/Runtime/VirtualController.prefab"));
        ManagerBase mb = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<ManagerBase>("Assets/Prefabs/Runtime/ManagerBase.prefab"));
        DialogueBoxManager dbm = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<DialogueBoxManager>("Assets/Prefabs/Runtime/DialogueBoxManager.prefab"));
        GameObject coreLogic = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Runtime/CoreLogic.prefab"));

        vc.name = "###VIRTUALCONTROLLER###";
        mb.name = "###MANAGERBASE###";
        dbm.name = "###DIALOGUEBOXMANAGER###";

        coreLogic.transform.SetParent(playableBase.transform);
        vc.transform.SetParent(playableBase.transform);
        mb.transform.SetParent(playableBase.transform);
        dbm.transform.SetParent(playableBase.transform);
        mb.dialogueBoxManager = dbm;

    }
}

[InitializeOnLoadAttribute]
public static class RegisterPlayable
{
    // register an event handler when the class is initialized
    static RegisterPlayable()
    {
        EditorApplication.playModeStateChanged += HelperMenu.OnPlayStateChange;
    }

}