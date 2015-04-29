using UnityEngine;
using System.Collections;

public class ScrollArea : MonoBehaviour {

	public GameObject scrollbar; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnScrollbarChange(float val)
	{
		RectTransform myRect = (RectTransform)this.transform;
		//this.transform. (myRect.rect.x, myRect.rect.y, myRect.rect.width, myRect.rect.height); 

	}
}
