using UnityEngine;
using System.Collections;

public class StretchToCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var sr = this.GetComponent<SpriteRenderer> ();
		if (sr == null) return;
		
		this.transform.localScale = new Vector3(1,1,1);

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		
		float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		Vector3 sc = new Vector3 (worldScreenWidth / width, worldScreenHeight / height, 1);
		//transform.localScale.x = worldScreenWidth / width;
		//transform.localScale.y = worldScreenHeight / height;
		transform.localScale = sc;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
