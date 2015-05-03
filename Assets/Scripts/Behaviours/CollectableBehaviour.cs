using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour {
	public bool m_respawn;
	private bool canBeam = true;
	private EventPublisher eventPublisher;
	GameObject player;

	//need to have Start() so we can disable this.
	void Start()
	{
		if (GameObject.Find ("Level") != null) {
			eventPublisher = GameObject.Find ("Level").GetComponent<EventPublisher> ();
		} else { 
			Debug.Log ("No level game object in scene: " + Application.loadedLevelName);
		}
		player = GameObject.Find ("Player");	
	}

	void OnCollisionEnter(Collision collision) {
		//we HAVE to check if enabled, I think enabled controls updating alone
		if(enabled && collision.gameObject.tag == "Player") {
			eventPublisher.publish ( new CollectableEvent(gameObject));
			gameObject.SetActive(false);
			if(m_respawn)
				Invoke("Reactivate", 1);
		}
	}

	void Update()
	{
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		if (rigidBody != null) {
			Vector3 toPlayer = player.transform.position - transform.position;
			//cannot equal 0
			float radius = 6;
			float min = 0.45f, max = 1.0f;
			float range = max - min;
			float percentage = (radius - toPlayer.magnitude) / radius;
			if(percentage > 0)
			{
				percentage = min + (percentage*range);
				float multiplier = 100;
				toPlayer.Normalize();
				rigidBody.AddForce(toPlayer * percentage * multiplier);
			}
		}
	}

	void Reactivate()
	{
		gameObject.SetActive(true);
		eventPublisher.publish ( new CollectableEvent(gameObject, true));
	}

	public bool getBeam() 
	{
		return canBeam;
	}
}
