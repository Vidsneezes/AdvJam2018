using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour {

    public Text actorName;
    public Text actorResponse;
    public Text pressITo;

	public void OpenDialog(string aname, string response)
    {
        pressITo.gameObject.SetActive(false);
        gameObject.SetActive(true);
        actorName.text = aname;
        actorResponse.text = response;
    }

    public void CloseDialog()
    {
        pressITo.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    public void ShowNextPrompt()
    {
        pressITo.gameObject.SetActive(true);
    }
}
