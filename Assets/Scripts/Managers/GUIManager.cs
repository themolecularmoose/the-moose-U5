using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	// Attributes
	public Font hudFont;
	public GameObject healthBar; 
	public GameObject energyBar; 
	public GameObject collectionText;
	public RectTransform healthVisual;

	public Texture box;
	public LevelManager level;
	public LevelLoader loader;
	public EventPublisher eventPublisher;
	ShipBehaviour shipBehaviour;

	// Initial x positions for the bars so it is known what the "full" position is. 
	float healthInitialXPos;
	float energyInitialXPos;

	float barWidth;
	Vector3 healthPos;
	private MouseLook xaxis;
	private MouseLook yaxis;
	private float sens = 15.0f;
	private bool paused = false;
	private bool menupaused = false;
	
	void OnPause( PauseEvent pe ){
		paused = !paused;
		menupaused = (pe.displayMenu && paused) ? true : false;
	}

	void OnGUI() {
		if (menupaused) {
			drawPauseMenu ();
		}
	}
	// Use this for initialization
	void Start () {
		var rectTransform = healthBar.GetComponent<RectTransform> ();
		// Set initial positions so we know what the maximum value is. 
		healthInitialXPos = rectTransform.position.x; 
		energyInitialXPos = rectTransform.position.x;

		healthPos = rectTransform.position;
		// Get the width of the bars - should be the same for both. 
		barWidth = rectTransform.rect.size.x;
		//barWidth *= 10; // Scaling. I'm not sure how to do this better at this time.

		if (GameObject.Find ("Utilities") != null) {
			loader = GameObject.Find ("Utilities").GetComponent<LevelLoader> ();
		} else { 
			Debug.Log ("No loader game object in scene: " + Application.loadedLevelName);
		}
		if (GameObject.Find ("Level") != null) {
			eventPublisher = GameObject.Find ("Level").GetComponent<EventPublisher> ();
		} else { 
			Debug.Log ("No level game object in scene: " + Application.loadedLevelName);
		}
		var ship = GameObject.Find ("Player");
		xaxis = ship.GetComponent<MouseLook> ();
		yaxis = GameObject.Find ("Attachments").GetComponent<MouseLook> ();
		shipBehaviour = ship.GetComponent<ShipBehaviour> ();
		sens = xaxis.sensitivityX;
	}
	
	// Update is called once per frame
	void Update () {
		//healthBar.transform.position = healthPos;
		float x = shipBehaviour.Health / shipBehaviour.MaxHealth;
		healthVisual.anchorMax = new Vector2 (x, healthVisual.anchorMax.y);
	}

	public void UpdateCollectedMolecules(ArrayList collected)
	{
		//Text t = collectionText.GetComponent<Text> ();
	
		//t.text = collected.Count.ToString ();
	}

	public void drawPauseMenu(){
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), box);
		GUIStyle topCenterStyle = GUI.skin.GetStyle("Label");
		topCenterStyle.alignment = TextAnchor.UpperCenter;
		topCenterStyle.fontSize = 48;
		topCenterStyle.font = hudFont;
		topCenterStyle.normal.textColor = Color.yellow;
		//top center
		GUI.Label (new Rect (Screen.width / 2 - 100, 25, 200, 100), "PAUSED", topCenterStyle);
		GUIStyle bottomCenterStyle = GUI.skin.GetStyle("Label");
		bottomCenterStyle.alignment = TextAnchor.MiddleCenter;
		bottomCenterStyle.fontSize = 48;
		bottomCenterStyle.font = hudFont;
		bottomCenterStyle.normal.textColor = Color.white;
		bottomCenterStyle.hover.textColor = Color.yellow;
		if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 6 * 5 - 50, 300, 100), "MAIN MENU", bottomCenterStyle)){
			if( loader != null ){
				eventPublisher.publish ( new PauseEvent(true) );
				eventPublisher.publish ( new ShowMouseEvent(true) );
				loader.LoadLevel("start_menu");
			} else {
				Debug.Log ("No level loader found in scene: " + Application.loadedLevelName);
			}
		}
		if ( GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 6 * 4 - 50, 300, 100), "LEVEL SELECT", bottomCenterStyle) ){
			if( loader != null ){
				eventPublisher.publish ( new PauseEvent(true) );
				eventPublisher.publish ( new ShowMouseEvent(false) );
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
		sliderStyle.font = hudFont;
		sliderStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width * (sens + 50) / 200 - 30, Screen.height * 5 / 12 + 5, 60, 40),""+(int)(sens*10), sliderStyle);
		xaxis.sensitivityX = sens;
		yaxis.sensitivityY = sens;
	}

	public void UpdateHealthBar(float health, float maxHealth)
	{
		float healthPercLost = (1 - health / maxHealth);
		// Set the position of each to the initial minus the percentage of the width lost
		float healthX = healthInitialXPos - (healthPercLost* barWidth);
		healthPos.x = healthX;
	}

	// TODO: refactor out beam energy and remove function
	// Update position of energy and health bars. 
	// float health: Value of player's health. 
	// float maxHealth: Maximum value of player's health. 
	// float energy: Value of player's energy. 
	// float maxEnergy: Maximum of player's energy. 
	public void UpdateGUI(Vector4 info)
	{
		float health = info.x; float maxHealth = info.y; 
		float energy = info.z; float maxEnergy = info.w;

		// Get health percentage
		float healthPercLost = (1 -health / maxHealth); // correct
		// Get energy percentage
		float energyPercLost = (1- energy / maxEnergy); 

		// Set the position of each to the initial minus the percentage of the width lost
		float healthX = healthInitialXPos - (healthPercLost* barWidth); 
		float energyX = energyInitialXPos + (energyPercLost * barWidth); // add because it's scaled backwards

		// Get the positions as vector3s, modify X position, and then save new position
		Vector3 healthPos = healthBar.transform.position; 
		healthPos.x = healthX; 

		Vector3 energyPos = energyBar.transform.position; 

		energyPos.x = energyX; 

		Debug.Log ("Health Pos: " + healthPos);
		healthBar.transform.position = healthPos; 
		energyBar.transform.position = energyPos; 
	}

}
