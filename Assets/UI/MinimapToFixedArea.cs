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
		Debug.Log ("Canvas width: " + canRect.rect.width);
		// set width and height of the camera box
		//rt.rect.width rt.Rect.height
		//this.GetComponent<Camera>().pixelRect.width = rt.rect.width; 
		//this.GetComponent<Camera>().pixelRect.height = rt.rect.height; 

		// Get screen size in pixels, make ratio for viewport size
		float offsetX = (rt.rect.x / canRect.rect.x ); 
		float offsetY = (rt.rect.y / canRect.rect.y);
		float wid = rt.rect.width * rt.localScale.x;
		float hei = rt.rect.height * rt.localScale.y;
		Debug.Log ("Screen width ratio: "  + wid);
		float z = (float)(offsetX + (.5 * offsetX));
		float p = (float)(offsetY - (.5 * offsetY));
	//	this.GetComponent<Camera>().pixelRect.Set(0,0, rt.rect.width/2, rt.rect.height/2); 
		cam.rect = new Rect(z,p, wid, hei);
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
