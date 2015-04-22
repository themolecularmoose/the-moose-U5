﻿using UnityEngine;
using System.Collections;

public class BeamBehavior : MonoBehaviour {

	private bool stick = false;
	private bool jointed = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Tractor Beam"))
		{
			stick = true;
		}
		else
		{
			stick = false;
			DeleteJoint();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(stick && !jointed)
		{
			CreateJoint(other.GetComponent<Rigidbody>());
		}
	}

	private void CreateJoint(Rigidbody rb)
	{
		FixedJoint joint = gameObject.AddComponent<FixedJoint> ();
		joint.connectedBody = rb;
		jointed = true;
	}

	private void DeleteJoint()
	{
		FixedJoint joint = gameObject.GetComponent<FixedJoint> ();
		Destroy (joint);
		jointed = false;
	}
}
