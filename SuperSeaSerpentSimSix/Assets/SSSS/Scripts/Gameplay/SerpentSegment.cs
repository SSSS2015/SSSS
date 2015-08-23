using UnityEngine;
using System.Collections;

public class SerpentSegment : MonoBehaviour {
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

	public void AttachTo(GameObject attachTarget)
	{
		mJoint.connectedBody = attachTarget.GetComponent<Rigidbody>();
	}
}
