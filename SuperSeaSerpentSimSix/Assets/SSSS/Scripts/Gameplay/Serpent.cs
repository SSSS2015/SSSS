using UnityEngine;
using System.Collections.Generic;

public class Serpent : SerpentSegment {

	public float mSpeedMultiplier = 2.0f;
	public float mGravityAcceleration = 9.8f;

	public int mNumInitialSegments = 3;

	public GameObject mSegmentPrefab;
	public GameObject mTailPrefab;

	protected Vector3 mDesiredPos;

	protected LinkedList<SerpentSegment> mSegments = new LinkedList<SerpentSegment>();

	public void Start()
	{
		mDesiredPos = transform.position;

		GameObject attachTarget = gameObject;
		for(int i = 0; i < mNumInitialSegments; ++i)
		{
			SerpentSegment segment = CreateSegment((i != mNumInitialSegments-1)?mSegmentPrefab:mTailPrefab, attachTarget);
			attachTarget = segment.gameObject;
			mSegments.AddLast(new LinkedListNode<SerpentSegment>(segment));
		}
	}

	public SerpentSegment CreateSegment(GameObject prefab, GameObject attachTarget)
	{
		GameObject segmentObj = Instantiate(prefab, attachTarget.transform.position, attachTarget.transform.rotation) as GameObject;
		SerpentSegment segment = segmentObj.GetComponent<SerpentSegment>();
		segment.AttachTo(attachTarget);
		return segment;
	}

	public void Update()
	{
		//mHeadModel.transform.LookAt(mDesiredPos);
		transform.LookAt(mDesiredPos);
		/*
		foreach(var segment in mSegments)
		{
			segment.mRigidbody.AddForce(mRigidbody.velocity * 0.5f);
		}
		*/
	}

	public void FixedUpdate()
	{
		bool isAboveWater = transform.localPosition.magnitude >= 50.0f;
		if(isAboveWater)
		{
			Vector3 gravityDir = transform.position;
			gravityDir.Normalize();
			gravityDir *= -1;
			mRigidbody.AddForce(gravityDir*mGravityAcceleration, ForceMode.Acceleration);
			return;
		}

		Vector3 desiredDir = mDesiredPos - transform.position;
		float distFromDesiredPos = desiredDir.magnitude;
		desiredDir.Normalize ();
		Vector3 desiredVel = desiredDir * distFromDesiredPos * mSpeedMultiplier;// * Time.deltaTime;
		mRigidbody.velocity = Vector3.Lerp(mRigidbody.velocity, desiredVel, Time.deltaTime);
		//mRigidbody.AddForce(vel, ForceMode.VelocityChange);
	}
	
	public void MoveToward(Vector3 worldPos)
	{
		mDesiredPos = worldPos;
	}
}
