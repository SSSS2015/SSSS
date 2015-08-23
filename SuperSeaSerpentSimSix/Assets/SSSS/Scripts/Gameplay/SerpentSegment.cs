using UnityEngine;
using System.Collections;

public class SerpentSegment : BaseSeaCreature {
	public Joint mJoint;

	public override void Awake()
	{
		base.Awake();
		if(mJoint == null)
		{
			mJoint = GetComponent<Joint>();
		}
	}

	public void AttachTo(GameObject attachTarget)
	{
		mJoint.connectedBody = attachTarget.GetComponent<Rigidbody>();
	}
}
