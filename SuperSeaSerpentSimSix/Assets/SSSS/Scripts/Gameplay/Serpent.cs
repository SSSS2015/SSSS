using UnityEngine;
using System.Collections.Generic;

public class Serpent : SerpentSegment {

	public float mSpeedMultiplier = 2.0f;
	public float mGravityAcceleration = 9.8f;

	public float mMaxTurnAngle = Mathf.PI*0.5f;
	//public float mMaxAcceleration = 1.0f;

	public int mNumInitialSegments = 3;

	public GameObject mSegmentPrefab;
	public GameObject mTailPrefab;

	protected Vector3 mDesiredPos;

	protected LinkedList<SerpentSegment> mSegments = new LinkedList<SerpentSegment>();

	public float mUndulationForce = 10.0f;
	public float mUndulationTime = 1.0f;
	protected float mUndulationTimer = 0.0f; 

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
		segmentObj.transform.parent = World.Instance.transform;
		SerpentSegment segment = segmentObj.GetComponent<SerpentSegment>();
		segment.AttachTo(attachTarget);
		return segment;
	}

	[ContextMenu("Add Segment")]
	public void AddSegment()
	{
		AddSegment(mSegmentPrefab);
	}

	public void AddSegment(GameObject prefab)
	{
		LinkedListNode<SerpentSegment> tailNode = mSegments.Last;
		LinkedListNode<SerpentSegment> prevNode = tailNode.Previous;

		SerpentSegment segment = CreateSegment(prefab, prevNode.Value.gameObject);
		tailNode.Value.AttachTo(segment.gameObject);

		mSegments.AddBefore(tailNode, segment);
	}

	public void FixedUpdate()
	{
		bool isAboveWater = transform.localPosition.magnitude >= World.Instance.SeaLevel;
		if(isAboveWater)
		{
			Vector3 gravityDir = transform.position;
			gravityDir.Normalize();
			gravityDir *= -1;
			mRigidbody.AddForce(gravityDir*mGravityAcceleration, ForceMode.Acceleration);
		}
		else
		{
			Vector3 desiredDir = mDesiredPos - transform.position;
			float distFromDesiredPos = desiredDir.magnitude;
			if(distFromDesiredPos > 0)
			{
				desiredDir.Normalize ();
				//Vector3 desiredVel = desiredDir * mSpeedMultiplier;// * Time.deltaTime;
				Vector3 vel = mRigidbody.velocity;
				float currSpeed = vel.magnitude;
				Vector3 currDir = vel.normalized;

				Vector3 finalDir = Vector3.RotateTowards(currDir, desiredDir, mMaxTurnAngle, float.MaxValue);

				if(distFromDesiredPos > 0.1f)
				{
					mUndulationTimer += Time.deltaTime;
					Vector3 cross = Vector3.Cross(Camera.main.transform.forward, finalDir);
					cross.Normalize();
					float undulation = Mathf.Sin(Mathf.PI*1.5f*mUndulationTimer);
					finalDir += cross*undulation*0.25f;
				}

				Vector3 desiredVel = finalDir * distFromDesiredPos * mSpeedMultiplier;// * Time.deltaTime;
				Vector3.SmoothDamp(vel, desiredVel, ref vel, 1.0f, float.MaxValue, Time.deltaTime);
				//mRigidbody.velocity = Vector3.RotateTowards(mRigidbody.velocity, desiredVel, mMaxTurnAngle, mMaxAcceleration*Time.deltaTime);
				//mRigidbody.AddForce(vel, ForceMode.VelocityChange);


				/*
				if(mUndulationTimer >= mUndulationTime)
				{
					Vector3 undulateForceVec = transform.position.normalized;
					mRigidbody.AddForce(undulateForceVec*mUndulationForce, ForceMode.Force);
					mUndulationTimer -= mUndulationTime;
				}
				*/
				mRigidbody.velocity = vel;
			}
		}

		if(mRigidbody.velocity.sqrMagnitude > 0)
		{
			transform.LookAt(mRigidbody.position+mRigidbody.velocity);
		}
	}
	
	public void MoveToward(Vector3 worldPos)
	{
		mDesiredPos = worldPos;
	}
}
