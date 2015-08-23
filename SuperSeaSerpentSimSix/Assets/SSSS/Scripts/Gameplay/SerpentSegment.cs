using UnityEngine;
using System.Collections;

public class SerpentSegment : MonoBehaviour {
	public float mGravity = 9.8f;

	public Joint mJoint;
	public Rigidbody mRigidbody;

	public GameObject mModel;

	public void Awake()
	{
		if(mJoint == null)
		{
			mJoint = GetComponent<Joint>();
		}
		if(mRigidbody == null)
		{
			mRigidbody = GetComponent<Rigidbody>();
		}
	}

	public void ApplyGravity()
	{
		Vector3 gravityDir = transform.position;
		gravityDir.Normalize();
		gravityDir *= -1;
		mRigidbody.AddForce(gravityDir*mGravity, ForceMode.Acceleration);
	}

	public void AttachTo(GameObject attachTarget)
	{
		mJoint.connectedBody = attachTarget.GetComponent<Rigidbody>();
	}

	public virtual void FixedUpdate()
	{
		if(IsAboveWater())
		{
			ApplyGravity();
		}
	}

	public bool IsAboveWater()
	{
		return transform.localPosition.magnitude >= World.Instance.SeaLevel;
	}
}
