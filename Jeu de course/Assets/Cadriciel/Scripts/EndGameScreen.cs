using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour {

	Text rankings;

	// Use this for initialization
	void Start () {
		rankings = GetComponent<Text> ();
		// rankings.text = "RANKINGS\n\n";

		int playerCount = PlayerPrefs.GetInt ("PlayerCount");

		for (int i = 0; i < playerCount; ++i) {
			rankings.text += (PlayerPrefs.GetString ("position " + i.ToString())) + "\n";
		}
	}
	
	public void PlayAgain()
    {
        Application.LoadLevel("course");
    }
}
