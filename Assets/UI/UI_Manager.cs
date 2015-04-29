using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour {

	// Attributes
	public GameObject healthBar; 
	public GameObject energyBar; 

	public Text missionText; 

	// Initial x positions for the bars so it is known what the "full" position is. 
	float healthInitialXPos;
	float energyInitialXPos;

	float barWidth;

	Vector3 healthPos; 
	Vector3 energyPos; 

	// Use this for initialization
	void Start () {
		Debug.Log ("UI Manager Started."); 
		var rectTransform = healthBar.GetComponent<RectTransform> ();
		// Set initial positions so we know what the maximum value is. 
		healthInitialXPos = rectTransform.position.x; 
		energyInitialXPos = rectTransform.position.x;
		
		//healthPos = rectTransform.position;
		// Get the width of the bars - should be the same for both. 
		barWidth = rectTransform.rect.width; // good for both
		barWidth *= rectTransform.localScale.x;
		Debug.Log ("Bar width: " + barWidth);
		UpdateMissionText ("Read the tips!\nFamiliarize yourself with controls!");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void UpdateMissionText(string t)
	{
		missionText.text = t;
	}

	public void UpdateHealthBar(float health, float maxHealth)
	{
		Debug.Log ("Update Health bar.");
		Debug.Log (health); 
		Debug.Log (maxHealth);
		float healthPercLost = (1 - health / maxHealth);
		// Set the position of each to the initial minus the percentage of the width lost
		float healthX = healthInitialXPos - (healthPercLost* barWidth);
		//healthPos.x = healthX;

		Vector3 healthPoss = healthBar.transform.position; 
		healthPoss.x = healthX; 

		healthBar.transform.position = healthPoss; 
	}

	// TODO: refactor out beam energy and remove function
	// Update position of energy and health bars. 
	// float health: Value of player's health. 
	// float maxHealth: Maximum value of player's health. 
	// float energy: Value of player's energy. 
	// float maxEnergy: Maximum of player's energy. 
	public void UpdateGUI(Vector4 info)
	{
		Debug.Log ("Update GUI");
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

	public void UpdateCollectedMolecules(ArrayList collected)
	{
		//Text t = collectionText.GetComponent<Text> ();
		
		//t.text = collected.Count.ToString ();
	}
}
