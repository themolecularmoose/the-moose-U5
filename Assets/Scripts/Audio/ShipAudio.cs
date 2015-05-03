using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ShipAudio : MonoBehaviour {
	
	public AudioClip damage;
	public AudioClip boost;
	public AudioSource hover;
	public AudioSource ship;
	public AudioClip explode;
	public AudioClip shoot;
	public float busterVolume = 1.0f;
	public float minVolume = 0.0f;
	public float maxVolume = 0.2f;
	public float scale = 0.1f;
	
	public void PlayDamage()
	{
		ship.PlayOneShot(damage);
	}
	
	public void PlayBoost()
	{
		ship.PlayOneShot(boost);
	}
	
	public void PlayBuster ()
	{
		ship.PlayOneShot (shoot, busterVolume);
	}
	
	public void IncVol()
	{
		ship.volume = 0.6f;
	}
	
	public void DecVol()
	{
		ship.volume = 0.2f;
	}
	
	public void ToggleEngine(bool death)
	{
		if (death)
		{
			ship.Stop ();
			ship.PlayOneShot (explode, 1);
		}
		else
		{
			ship.Play ();
		}
	}

	void LateUpdate()
	{
		//DELTA: Scale volume by speed.
		//TODO: Store rigid body in class variable
		Rigidbody r = GetComponent<Rigidbody> ();
		float range = maxVolume - minVolume;
		float vol = Mathf.Clamp(r.velocity.sqrMagnitude * range * scale, minVolume, maxVolume);
		hover.volume = vol;
	}
}
