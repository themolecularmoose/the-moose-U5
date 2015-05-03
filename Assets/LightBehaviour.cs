using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {

	private float sec;
	private int nflick;
	private Light light;
	// Use this for initialization
	void Start () {
		light = gameObject.GetComponent<Light> ();
		StartCoroutine (flicker ());
	}

	public IEnumerator flicker()
	{
		while(true)
		{
			//time until next set of flickers
			sec = Random.Range (1, 7);
			//how many flickers
			nflick = (int)Random.Range (1, 4);
			yield return new WaitForSeconds(sec);
			//flicker
			for(int i = 0; i < nflick; i++)
			{
				light.enabled = false;
				yield return new WaitForSeconds(0.1f);
				light.enabled = true;
				yield return new WaitForSeconds(0.1f);
			}
		}
		
	}
}
