using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// GUI manager.
/// Updates the in-game heads up display;
/// Updating the resource bars to reflect the player status.
/// Also draws the pause menu.
/// </summary>
//TODO: Make Pause Menu a canvas component or separate class or both.
//Pausing should be available in any level, including the mothership
//TODO: Remove commented-out code
public class GUIManager : MonoBehaviour {

	// Attributes
	//Used for the pause menu
	public Font hudFont;
	//public GameObject healthBar; 
	//public GameObject energyBar; 
	//TODO: Update the collection text based on collectable count and collected count
	public GameObject collectionText;
	//Modify these containers to represent health and energy changes
	public RectTransform healthVisual;
	public RectTransform energyVisual;
	//Use a dictionary to associate a texture with a molecule
	public Texture waterTexture;
	public Texture methaneTexture;
	private Dictionary<string, Texture> moleculeForTexture;
	public Text collectedText;
	public Text totalCollectableText;

	//Use for pause menu to draw backgrounds
	public Texture box;
	//Use to access collectable information
	private LevelManager level;
	//Use with pause menu to change scenes
	private LevelLoader loader;
	//Use with pause menu to unpause the game
	private EventPublisher eventPublisher;
	//Use to update the health and energy visuals
	private ShipBehaviour shipBehaviour;

	// Initial x positions for the bars so it is known what the "full" position is. 
	//float healthInitialXPos;
	//float energyInitialXPos;

	//float barWidth;
	//Vector3 healthPos;
	//Use to get and set the ship sensitivity
	private MouseLook xaxis;
	private MouseLook yaxis;
	private float sens = 15.0f;
	private bool paused = false;
	private bool menupaused = false;

	/// <summary>
	/// Reciever for sendMessage("OnPause") which brings up the pause menu
	/// </summary>
	/// <param name="pe"> A pause event with boring data</param>
	void OnPause( PauseEvent pe ){
		paused = !paused;
		menupaused = (pe.displayMenu && paused) ? true : false;
	}

	/// <summary>
	/// Draws the pause menu
	/// </summary>
	void OnGUI() {
		if (menupaused) {
			drawPauseMenu ();
		}
	}

	// Use this for initialization
	/// <summary>
	/// Loads system components and finds the droid ship + its mouse components.
	/// </summary>
	void Start () {
		/*var rectTransform = healthBar.GetComponent<RectTransform> ();
		// Set initial positions so we know what the maximum value is. 
		healthInitialXPos = rectTransform.position.x; 
		energyInitialXPos = rectTransform.position.x;

		healthPos = rectTransform.position;
		// Get the width of the bars - should be the same for both. 
		barWidth = rectTransform.rect.size.x;
		//barWidth *= 10; // Scaling. I'm not sure how to do this better at this time.
		*/

		//find or create these system components
		level = ObjectFinder.FindOrCreateComponent<LevelManager> ();
		loader = ObjectFinder.FindOrCreateLevelLoader ();
		eventPublisher = ObjectFinder.FindOrCreateComponent<EventPublisher> ();

		//find the player to get the the mouse components and ship behaviour
		var ship = GameObject.Find ("Player");
		xaxis = ship.GetComponent<MouseLook> ();
		yaxis = GameObject.Find ("Attachments").GetComponent<MouseLook> ();
		shipBehaviour = ship.GetComponent<ShipBehaviour> ();

		//initialize the sensitivity to whatever is currently set in the editor
		sens = xaxis.sensitivityX;
		//initialize our molecule_tag -> texture mapping
		moleculeForTexture = new Dictionary<string, Texture>(){
			{"Water", waterTexture},
			{"Methane", methaneTexture}
		};
	}
	
	// Update is called once per frame
	/// <summary>
	/// Used to update the health and energy bars
	/// </summary>
	void Update () {
		//healthBar.transform.position = healthPos;

		//find the percentage of player health
		float healthPercentage = shipBehaviour.Health / shipBehaviour.MaxHealth;
		//set the visual to squish horizontally according to that percentage
		healthVisual.anchorMax = new Vector2 (healthPercentage, healthVisual.anchorMax.y);

		//get all collectables
		int collected = level.Collected.Count;
		//get collected molecules
		int collectables = level.Collectables.Count;
		//set our collected text to match (if its single digits add a 0)
		//TODO: do real formatting using C# stuff
		collectedText.text = (collected > 9 ? "" : "0") + collected.ToString();
		totalCollectableText.text = (collectables > 9 ? "" : "0") + collectables.ToString();
		//go through each type of collected molecule

		//if we have any of this type updated a collectedPanel
	}

	public void UpdateCollectedMolecules(ArrayList collected)
	{
		//Text t = collectionText.GetComponent<Text> ();
	
		//t.text = collected.Count.ToString ();
	}

	/// <summary>
	/// Renders the pause menu
	/// Also waits for user input to change sensitivity, go back to level select, or go back to main menu
	/// </summary>
	public void drawPauseMenu(){
		//draw a dark background
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), box);

		//draw a label indicating we are paused
		GUIStyle topCenterStyle = GUI.skin.GetStyle("Label");
		topCenterStyle.alignment = TextAnchor.UpperCenter;
		topCenterStyle.fontSize = 48;
		topCenterStyle.font = hudFont;
		topCenterStyle.normal.textColor = Color.yellow;
		//top center
		GUI.Label (new Rect (Screen.width / 2 - 100, 25, 200, 100), "PAUSED", topCenterStyle);

		//create 2 buttons for main menu and level select
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

		//Create a label and slider for changing mouse sensitivity
		GUI.Label (new Rect (0, Screen.height / 6 * 2 - 50, Screen.width, 100), "MOUSE SENSITIVITY", bottomCenterStyle);
		Rect sliderBoxOutside = new Rect (Screen.width * 1 / 4, Screen.height * 5 / 12, Screen.width / 2, 50);
		sens = GUI.HorizontalSlider (sliderBoxOutside, sens, 0.0F, 100.0F);
		GUIStyle sliderStyle = GUI.skin.GetStyle("Label");
		sliderStyle.alignment = TextAnchor.MiddleCenter;
		sliderStyle.fontSize = 24;
		sliderStyle.font = hudFont;
		sliderStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width * (sens + 50) / 200 - 30, Screen.height * 5 / 12 + 5, 60, 40),""+(int)(sens*10), sliderStyle);

		//set the sensitivities to the slider value
		xaxis.sensitivityX = sens;
		yaxis.sensitivityY = sens;
	}

	/*public void UpdateHealthBar(float health, float maxHealth)
	{
		float healthPercLost = (1 - health / maxHealth);
		// Set the position of each to the initial minus the percentage of the width lost
		float healthX = healthInitialXPos - (healthPercLost* barWidth);
		healthPos.x = healthX;
	}*/

	// TODO: refactor out beam energy and remove function
	// Update position of energy and health bars. 
	// float health: Value of player's health. 
	// float maxHealth: Maximum value of player's health. 
	// float energy: Value of player's energy. 
	// float maxEnergy: Maximum of player's energy. 
	/*public void UpdateGUI(Vector4 info)
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
	}*/

}
