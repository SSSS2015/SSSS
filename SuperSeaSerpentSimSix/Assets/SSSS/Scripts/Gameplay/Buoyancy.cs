using UnityEngine;
using System.Collections;

public class Buoyancy  : MonoBehaviour {

	public static float kGravity = 9.8f;

	public bool mBuoyancyEnabled = true;
	public float mBuoyanceFactor = 0.25f;
	public float mEndsOffset = 1.0f;

	public Rigidbody mRigidbody;

	public void Awake()
	{
		if(mRigidbody == null)
		{
			mRigidbody = GetComponent<Rigidbody>();
		}
	}

	public void FixedUpdate () 
	{
		HandleBuoyancy(transform.position + transform.right*mEndsOffset, 0.5f);
		HandleBuoyancy(transform.position - transform.right*mEndsOffset, 0.5f);
	}

	public void HandleBuoyancy(Vector3 worldPos, float optForceContribution = 1.0f)
	{
		float distFromSeaLevel = World.Instance.SeaLevel-0.5f - worldPos.magnitude;
		Vector3 gravityDir = transform.position;
		gravityDir.Normalize();
		gravityDir *= optForceContribution;

		if(distFromSeaLevel < 0 || !mBuoyancyEnabled)// above water
		{
			mRigidbody.AddForceAtPosition(gravityDir*-kGravity, worldPos, ForceMode.Acceleration);
		}
		else
		{
			mRigidbody.AddForceAtPosition(gravityDir*kGravity*distFromSeaLevel*mBuoyanceFactor, worldPos, ForceMode.Acceleration);
		}
	}
}
