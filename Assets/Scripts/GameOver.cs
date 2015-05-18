using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	private LevelLoader loader;

	// Use this for initialization
	void Start (){
		loader = GameObject.Find ("Utilities").GetComponent<LevelLoader> ();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	void OnGUI () {
		// Make a background box
		Rect rect = new Rect(0, 0, Screen.width, Screen.height);
		GUI.Box (rect, "");
		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = 48;
		centeredStyle.normal.textColor = Color.yellow;
		
		// centered and at top of screen
		GUI.Label (new Rect (rect.center[0]-100, rect.center[1]-200, 200, 150), "Final Score");
		GUI.Label (new Rect (rect.center[0]-100, rect.center[1]-80, 200, 100), "5/5");

		string victory = "";
		victory = "Winner";

		GUI.Label (new Rect (rect.center[0]-100, rect.center[1]+100, 200, 100), victory);
		if (GUI.Button (new Rect (rect.center[0]-50,rect.center[1],110,20), "Return To Base")) {
			loader.LoadLevel ("level_select");
		}
		
		// Make the second button.
		if (GUI.Button (new Rect (rect.center[0] -50,rect.center[1] + 40,110,20), "Quit")) {
			loader.LoadLevel("start_menu");
		} 
	}
}

