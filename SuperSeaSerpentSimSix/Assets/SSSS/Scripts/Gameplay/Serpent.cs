using UnityEngine;
using System.Collections.Generic;

public class Serpent : SerpentSegment {

	public static readonly Vector3 kSegmentGrowthScale = new Vector3(1.75f, 1.75f, 1.75f);

	public float mSpeedMultiplier = 2.0f;

	public float mMaxTurnAngle = Mathf.PI*0.5f;
	public float mMaxAcceleration = 10.0f;

	public int mNumInitialSegments = 3;

	public GameObject mSegmentPrefab;
	public GameObject mTailPrefab;

	protected Vector3 mDesiredPos;
	public float mReentryInputBlockerTime = 0.5f;
	protected float mReentryInputBlockerTimer = 0.0f;

	protected LinkedList<SerpentSegment> mSegments = new LinkedList<SerpentSegment>();

	public float mUndulationForce = 10.0f;
	public float mUndulationTime = 1.0f;
	protected float mUndulationTimer = 0.0f; 

	public float mSegmentGrowthProgressTime = 0.5f;
	protected float mSegmentGrowthProgressTimer = 0.0f;

	public class SegmentGrowth
	{
		public LinkedListNode<SerpentSegment> mCurrentSegment;
		public GameObject mSegmentPrefab;
	}
	public HashSet<SegmentGrowth> mSegmentGrowths = new HashSet<SegmentGrowth>();

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
	public void GrowSegment(GameObject prefab)
	{
		SegmentGrowth growth = new SegmentGrowth();
		growth.mSegmentPrefab = prefab;
		growth.mCurrentSegment = mSegments.First;

		mSegmentGrowths.Add(growth);
	}

	public void ProgressSegmentGrowth()
	{
		HashSet<SegmentGrowth> completedGrowths = new HashSet<SegmentGrowth>();
		foreach(var growth in mSegmentGrowths)
		{
			growth.mCurrentSegment = growth.mCurrentSegment.Next;
			if(growth.mCurrentSegment == mSegments.Last)
			{
				AddSegment(growth.mSegmentPrefab);
				completedGrowths.Add(growth);
			}
		}

		foreach(var growth in completedGrowths)
		{
			mSegmentGrowths.Remove(growth);
		}
	}

	public void UpdateSegmentGrowthSizes()
	{
		foreach(var growth in mSegmentGrowths)
		{
			float t = Mathf.Clamp01(mSegmentGrowthProgressTimer/mSegmentGrowthProgressTime);
			growth.mCurrentSegment.Value.mModel.transform.localScale = Vector3.Lerp(kSegmentGrowthScale, Vector3.one, t);
			if(growth.mCurrentSegment.Next != mSegments.Last)
			{
				growth.mCurrentSegment.Next.Value.mModel.transform.localScale = Vector3.Lerp(Vector3.one, kSegmentGrowthScale, t);
			}
		}
	}

	public void AddSegment()
	{
		AddSegment(mSegmentPrefab);
	}

	public void AddSegment(GameObject prefab)
	{
		LinkedListNode<SerpentSegment> tailNode = mSegments.Last;
		LinkedListNode<SerpentSegment> prevNode = tailNode.Previous;

		SerpentSegment segment = CreateSegment(prefab, prevNode.Value.gameObject);
		segment.mModel.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
		tailNode.Value.AttachTo(segment.gameObject);

		mSegments.AddBefore(tailNode, segment);
	}

	public void Update()
	{
		if(mSegmentGrowths.Count <= 0)
		{
			mSegmentGrowthProgressTimer = 0.0f;
			return;
		}

		mSegmentGrowthProgressTimer += Time.deltaTime;
		if(mSegmentGrowthProgressTimer >= mSegmentGrowthProgressTime)
		{
			mSegmentGrowthProgressTimer -= mSegmentGrowthProgressTime;
			ProgressSegmentGrowth();
		}

		UpdateSegmentGrowthSizes();
	}

	public override void FixedUpdate()
	{
		if(IsAboveWater())
		{
			mReentryInputBlockerTimer = mReentryInputBlockerTime;
		}

		if(mReentryInputBlockerTimer > 0)
		{
			mReentryInputBlockerTimer -= Time.deltaTime;
			ApplyGravity();
		}
		else
		{
			Vector3 desiredDir = mDesiredPos - transform.position;
			float distFromDesiredPos = desiredDir.magnitude;
			if(distFromDesiredPos > 0.5f)
			{
				desiredDir.Normalize ();
				//Vector3 desiredVel = desiredDir * mSpeedMultiplier;// * Time.deltaTime;
				Vector3 vel = mRigidbody.velocity;
				//float currSpeed = vel.magnitude;
				Vector3 currDir = vel.normalized;

				Vector3 finalDir = Vector3.RotateTowards(currDir, desiredDir, mMaxTurnAngle, float.MaxValue);

				mUndulationTimer += Time.deltaTime;
				Vector3 cross = Vector3.Cross(Camera.main.transform.forward, finalDir);
				cross.Normalize();
				//float undulationSpeedFactor = Mathf.Clamp01(currSpeed/20.0f);
				float undulationPeriod = Mathf.PI*1.5f;// * (undulationSpeedFactor);
				float undulation = Mathf.Sin(undulationPeriod*mUndulationTimer);
				finalDir += cross*undulation*0.5f;

				Vector3 desiredVel = finalDir * distFromDesiredPos * mSpeedMultiplier;// * Time.deltaTime;
				Vector3.SmoothDamp(vel, desiredVel, ref vel, 1.0f, mMaxAcceleration, Time.deltaTime);
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

		UpdateLooking();
	}

	protected void UpdateLooking()
	{
		if(mRigidbody.velocity.sqrMagnitude > 0)
		{
			transform.LookAt(mRigidbody.position+mRigidbody.velocity, transform.position.normalized);
			/*
			Vector3 lookDir = mRigidbody.position+mRigidbody.velocity;
			lookDir -= transform.position;
			lookDir.Normalize();
			float deltaAngle = Vector3.Angle(transform.forward, lookDir);
			
			Vector3 cross = Vector3.Cross(Camera.main.transform.forward, transform.forward);
			float rotationDir = Mathf.Sign(Vector3.Dot(cross, lookDir));
			transform.Rotate(new Vector3(deltaAngle*rotationDir,0,0));
			*/
		}
	}
	
	public void MoveToward(Vector3 worldPos)
	{
		mDesiredPos = worldPos;
	}

	public void OnTriggerEnter(Collider c)
	{
		Fish fish = c.GetComponentInParent<Fish>();
		if(fish != null)
		{
			GrowSegment(mSegmentPrefab);
			Destroy(fish.gameObject);
		}
	}
}
