using UnityEngine;
using System;
using System.Collections;

public class PauseGUI : MonoBehaviour {
	bool isVisible;
	float sens = 15;
	//Inspector Variables
	public Texture box;
	public MouseLook xAxis;
	public MouseLook yAxis;
	//Scene Variables
	PauseComponent pause;
	LevelLoader loader;

	// Use this for initialization
	void Start () {
		//find or create pause so we can show or hide the pause menu
		pause = ObjectFinder.FindOrCreateComponent<PauseComponent> ();
		//Show the menu when theres a pause
		pause.OnPause += ShowMenu;
		//Hide the menu when theres an unpause
		pause.OnResume += HideMenu;
		//get a level loader so we quit
		loader = ObjectFinder.FindOrCreateLevelLoader ();
		if (xAxis != null)
			sens = xAxis.sensitivityX;
	}

	void ShowMenu(object sender, EventArgs e){
		isVisible = true;
	}
	void HideMenu(object sender, EventArgs e){
		isVisible = false;
	}


	void OnGUI () {
		if (isVisible) {
			drawPauseMenu();
		}
	}
	
	public void drawPauseMenu(){
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), box);
		GUIStyle topCenterStyle = GUI.skin.GetStyle("Label");
		topCenterStyle.alignment = TextAnchor.UpperCenter;
		topCenterStyle.fontSize = 48;
		//topCenterStyle.font = hudFont;
		topCenterStyle.normal.textColor = Color.yellow;
		//top center
		GUI.Label (new Rect (Screen.width / 2 - 100, 25, 200, 100), "PAUSED", topCenterStyle);
		GUIStyle bottomCenterStyle = GUI.skin.GetStyle("Label");
		bottomCenterStyle.alignment = TextAnchor.MiddleCenter;
		bottomCenterStyle.fontSize = 48;
		//bottomCenterStyle.font = hudFont;
		bottomCenterStyle.normal.textColor = Color.white;
		bottomCenterStyle.hover.textColor = Color.yellow;
		if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 6 * 5 - 50, 300, 100), "MAIN MENU", bottomCenterStyle)){
			if( loader != null ){
				//eventPublisher.publish ( new PauseEvent(true) );
				//eventPublisher.publish ( new ShowMouseEvent(true) );
				loader.LoadLevel("start_menu");
			} else {
				Debug.Log ("No level loader found in scene: " + Application.loadedLevelName);
			}
		}
		if ( GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 6 * 4 - 50, 300, 100), "LEVEL SELECT", bottomCenterStyle) ){
			if( loader != null ){
				//eventPublisher.publish ( new PauseEvent(true) );
				//eventPublisher.publish ( new ShowMouseEvent(false) );
				loader.LoadLevel("level_select");
			} else {
				Debug.Log ("No level loader found in scene: " + Application.loadedLevelName);
			}
		}
		GUI.Label (new Rect (0, Screen.height / 6 * 2 - 50, Screen.width, 100), "MOUSE SENSITIVITY", bottomCenterStyle);
		Rect sliderBoxOutside = new Rect (Screen.width * 1 / 4, Screen.height * 5 / 12, Screen.width / 2, 50);
		sens = GUI.HorizontalSlider (sliderBoxOutside, sens, 0.0F, 100.0F);
		GUIStyle sliderStyle = GUI.skin.GetStyle("Label");
		sliderStyle.alignment = TextAnchor.MiddleCenter;
		sliderStyle.fontSize = 24;
		//sliderStyle.font = hudFont;
		sliderStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width * (sens + 50) / 200 - 30, Screen.height * 5 / 12 + 5, 60, 40),""+(int)(sens*10), sliderStyle);
		if(xAxis != null)
			xAxis.sensitivityX = sens;
		if(yAxis != null)
			yAxis.sensitivityY = sens;
	}
}
