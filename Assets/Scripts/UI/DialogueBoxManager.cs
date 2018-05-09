using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour {

    public Text actorName;
    public Text actorResponse;

	public void OpenDialog(string aname, string response)
    {
        gameObject.SetActive(true);
        actorName.text = aname;
        actorResponse.text = response;
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }
}
