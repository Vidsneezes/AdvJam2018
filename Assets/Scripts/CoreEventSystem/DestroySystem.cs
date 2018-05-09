using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySystem : MonoBehaviour {

	public void DestroySelf()
    {
        GameObject.Destroy(gameObject);
    }
}
