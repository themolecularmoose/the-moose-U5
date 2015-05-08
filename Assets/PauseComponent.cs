using UnityEngine;
using System;

using System.Collections;

public class PauseComponent : MonoBehaviour {
	//WIP: Somehow pause things that don't depend on TimeScale
	public GameObject[] disableComponents;
	public string[] disableTypes;
	//Pause variables
	public bool paused { get; protected set; }
	//Callbacks
	public EventHandler OnPause;
	public EventHandler OnResume;
	//Scene variables
	LevelLoader loader;

	// Use this for initialization
	void Start () {
		loader = ObjectFinder.FindOrCreateLevelLoader ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			if(paused)
			{
				Resume();
			}else
			{
				Pause();
			}
		}
	}

	public void Pause()
	{
		paused = true;
		if(OnPause != null)
			OnPause (this, null);
		//disable the components of our targets
		for(int i = 0; i < disableComponents.Length; ++i)
		{
			GameObject current = disableComponents[i];
			//todo:get and disable components
			//check for already disabled
			//do not re-enable those which were not enabled initially
		}
	}

	public void Resume()
	{
		paused = false;
		if (OnResume != null)
			OnResume (this, null);
		//enable the components of our targets
		//todo: enabled the components which were not disabled before the pause
	}
}
