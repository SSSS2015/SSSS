using UnityEngine;
using System.Collections;

public class BaseSeaCreature : MonoBehaviour {
	public float mGravity = 9.8f;
	
	public GameObject mModel;
	public Rigidbody mRigidbody;

	public virtual void Awake()
	{
		if(mRigidbody == null)
		{
			mRigidbody = GetComponent<Rigidbody>();
		}
	}
	
	public virtual void FixedUpdate()
	{
		if(IsAboveWater())
		{
			ApplyGravity();
		}
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
		return transform.localPosition.magnitude >= World.Instance.SeaLevel;
	}
}
