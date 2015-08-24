using UnityEngine;
using System.Collections;

public class Buoyancy  : MonoBehaviour {

	public static float kGravity = 9.8f;

	public bool mBuoyancyEnabled = true;
	public float mBuoyanceFactor = 0.25f;
	public float mEndsOffset = 1.0f;

	public Rigidbody mRigidbody;
	
	protected bool mWasAboveWater = true;

	public void Awake()
	{
		if(mRigidbody == null)
		{
			mRigidbody = GetComponent<Rigidbody>();
		}
	}

	public void FixedUpdate () 
	{
		bool isAboveWater = IsAboveWater();

		HandleBuoyancy(transform.position + transform.right*mEndsOffset, 0.5f);
		HandleBuoyancy(transform.position - transform.right*mEndsOffset, 0.5f);

		if(mWasAboveWater != isAboveWater)
		{
			World.Instance.SpawnSplash(mRigidbody.velocity.magnitude, transform.position);
		}
		
		mWasAboveWater = isAboveWater;
	}

	public void HandleBuoyancy(Vector3 worldPos, float optForceContribution = 1.0f)
	{
		Vector2 polarCoords = World.Instance.GetPolarCoordinate(transform.position);
		float distFromSeaLevel = World.Instance.GetSeaLevel(polarCoords.y) - worldPos.magnitude;
		Vector3 gravityDir = transform.position;
		gravityDir.Normalize();
		gravityDir *= optForceContribution;

		if(distFromSeaLevel < 0)// above water
		{
			mRigidbody.AddForceAtPosition(gravityDir*-kGravity, worldPos, ForceMode.Acceleration);
		}
		else if(mBuoyancyEnabled)
		{
			mRigidbody.AddForceAtPosition(gravityDir*kGravity*distFromSeaLevel*mBuoyanceFactor, worldPos, ForceMode.Acceleration);
		}
		else
		{
			mRigidbody.AddForceAtPosition(gravityDir*-0.25f*kGravity, worldPos, ForceMode.Acceleration);
		}


	}

	public bool IsAboveWater()
	{
		Vector2 polarCoords = World.Instance.GetPolarCoordinate(transform.position);
		return polarCoords.x >= World.Instance.GetSeaLevel(polarCoords.y);
	}
}
