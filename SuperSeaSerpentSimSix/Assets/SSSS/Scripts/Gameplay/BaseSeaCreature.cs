using UnityEngine;
using System.Collections;

public class BaseSeaCreature : MonoBehaviour {
	public float mGravity = 9.8f;
	
	public GameObject mModel;
	public Rigidbody mRigidbody;

	protected bool mWasAboveWater = false;

	public virtual void Awake()
	{
		if(mRigidbody == null)
		{
			mRigidbody = GetComponent<Rigidbody>();
		}
	}
	
	public virtual void FixedUpdate()
	{
		bool isAboveWater = IsAboveWater();
		if(isAboveWater)
		{
			ApplyGravity();
		}

		if(mWasAboveWater != isAboveWater)
		{
			World.Instance.SpawnSplash(mRigidbody.velocity.magnitude, transform.position);
		}
		mWasAboveWater = isAboveWater;
	}
	
	public void ApplyGravity()
	{
		Vector3 gravityDir = transform.position;
		gravityDir.Normalize();
		gravityDir *= -1;
		mRigidbody.AddForce(gravityDir*mGravity, ForceMode.Acceleration);
	}
	
	public bool IsAboveWater()
	{
        Vector2 polarCoords = World.Instance.GetPolarCoordinate(transform.position);
		return polarCoords.x >= World.Instance.GetSeaLevel(polarCoords.y);
	}
}
