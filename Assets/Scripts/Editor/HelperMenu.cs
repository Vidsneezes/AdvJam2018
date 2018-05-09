using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HelperMenu : MonoBehaviour {

    [MenuItem("Level Editor/Player Spawn Point")]
    static void AddPlayerSpawnPoint()
    {
        PlayerSpawner[] ps = GameObject.FindObjectsOfType<PlayerSpawner>();
        if (ps.Length == 0)
        {
            PlayerSpawner ps_prefab = AssetDatabase.LoadAssetAtPath<PlayerSpawner>("Assets/Prefabs/LevelBuilding/PlayerSpawnPoint.prefab");
            PlayerSpawner ps_obj = GameObject.Instantiate(ps_prefab);
            ps_obj.transform.position = new Vector3(0, 0, 0);
            ps_obj.name = "###PLAYERSPAWNPOINT###";
        }
    }
}
