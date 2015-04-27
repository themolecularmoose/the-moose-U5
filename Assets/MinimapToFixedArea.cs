using UnityEngine;
using System.Collections;

public class MinimapToFixedArea : MonoBehaviour {

	public GameObject imageForArea; 
	public GameObject uiCanvas;
	public RectTransform rt; 
	public RectTransform canRect;
	public Camera cam; 

	// Use this for initialization
	void Start () {
		Debug.Log ("Old rect:");
		Debug.Log (this.GetComponent<Camera>().rect);
		// get the rect transform of our image
		rt = (RectTransform)imageForArea.transform; 

		// get canvas size
		canRect = (RectTransform)uiCanvas.transform; 

		// set width and height of the camera box
		Debug.Log ("Rect is width: " + rt.rect.width + ", and height: " + rt.rect.height);
		//rt.rect.width rt.Rect.height
		//this.GetComponent<Camera>().pixelRect.width = rt.rect.width; 
		//this.GetComponent<Camera>().pixelRect.height = rt.rect.height; 
		// Get screen size in pixels, make ratio for viewport size
		var wid = (rt.rect.width / canRect.rect.width ); 
		Debug.Log ("Screen width ratio: "  + wid);
	//	this.GetComponent<Camera>().pixelRect.Set(0,0, rt.rect.width/2, rt.rect.height/2); 
		cam.rect.Set(0,0,wid/2, 100);
		Debug.Log ("New rect:");
		Debug.Log (cam.rect);

	}
	
	// Update is called once per frame
	void Update () {
		// get canvas size
		RectTransform canRect = (RectTransform)uiCanvas.transform; 
		var wid = (rt.rect.width / canRect.rect.width ); 
		var hei = (rt.rect.height / canRect.rect.height);
		cam.rect.Set(0,0,wid, hei);
	}
}
