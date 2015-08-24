using UnityEngine;
using System.Collections.Generic;

public class Serpent : SerpentSegment {

	public static readonly Vector3 kSegmentGrowthScale = new Vector3(1.5f, 1.5f, 1.5f);
	public float mMaxSpeed = 10.0f;
	public float mSpeedMultiplier = 2.0f;
	public float mSpeedBonusPerSegment = 0.25f;

	public float mMaxTurnAngle = Mathf.PI*0.5f;

	public int mNumInitialSegments = 3;

	public GameObject mSegmentPrefab;
	public GameObject mTailPrefab;
	public GameObject mSkullPrefab;

	protected Vector3 mDesiredPos;
	public float mReentryInputBlockerTime = 0.5f;
	protected float mReentryInputBlockerTimer = 0.0f;

	protected LinkedList<SerpentSegment> mSegments = new LinkedList<SerpentSegment>();
	public LinkedList<SerpentSegment> Segments { get { return mSegments; } }

	public float mAttackForce = 30.0f;

	public float mUndulationForce = 10.0f;
	public float mUndulationTime = 1.0f;
	protected float mUndulationTimer = 0.0f; 

	public float mSegmentGrowthProgressTime = 0.5f;
	protected float mSegmentGrowthProgressTimer = 0.0f;

	protected int mHealth = 0;
	public int Health { get { return mHealth; } }
	public int MaxHealth { get { return mSegments.Count; } }

	protected int mNextGrowthNeed = 2;
	public int NextGrowthNeed { get { return mNextGrowthNeed; } }
	protected int mNextGrowthCounter = 0;
	public int NextGrowthCounter { get { return mNextGrowthCounter; } }

	public Animator mAnimator;

	public class SegmentGrowth
	{
		public LinkedListNode<SerpentSegment> mCurrentSegment;
		public GameObject mSegmentPrefab;
		public int mHealAmount;
	}
	public HashSet<SegmentGrowth> mSegmentGrowths = new HashSet<SegmentGrowth>();

	public override void Awake ()
	{
		base.Awake ();
		if(mAnimator == null)
		{
			mAnimator = GetComponentInChildren<Animator>();
		}
		mSerpent = this;

		mDesiredPos = transform.position;
		
		GameObject attachTarget = gameObject;
		for(int i = 0; i < mNumInitialSegments; ++i)
		{
			SerpentSegment segment = CreateSegment((i != mNumInitialSegments-1)?mSegmentPrefab:mTailPrefab, attachTarget);
			attachTarget = segment.gameObject;
			mSegments.AddLast(new LinkedListNode<SerpentSegment>(segment));
		}
		
		mHealth = mNumInitialSegments;
	}

	protected SerpentSegment CreateSegment(GameObject prefab, GameObject attachTarget)
	{
		GameObject segmentObj = Instantiate(prefab, attachTarget.transform.position, attachTarget.transform.rotation) as GameObject;
		SerpentSegment segment = segmentObj.GetComponent<SerpentSegment>();
		segment.mSerpent = this;
		segment.AttachTo(attachTarget);
		return segment;
	}

	public void Digest(int healAmount = 1)
	{
		GrowSegment(null, healAmount);
	}

	public void GrowSegment(int healAmount = 1)
	{
		GrowSegment(mSegmentPrefab, healAmount);
	}

	public void GrowSegment(GameObject prefab, int healAmount = 1)
	{
		SegmentGrowth growth = new SegmentGrowth();
		growth.mSegmentPrefab = prefab;
		growth.mCurrentSegment = mSegments.First;
		growth.mHealAmount = healAmount;

		mSegmentGrowths.Add(growth);
	}

	protected void ProgressSegmentGrowth()
	{
		HashSet<SegmentGrowth> completedGrowths = new HashSet<SegmentGrowth>();
		foreach(var growth in mSegmentGrowths)
		{
			growth.mCurrentSegment = growth.mCurrentSegment.Next;
			if(growth.mCurrentSegment == mSegments.Last)
			{
				if(growth.mSegmentPrefab != null)
				{
					++mNextGrowthCounter;
					if(mNextGrowthCounter >= mNextGrowthNeed)
					{
						AddSegment(growth.mSegmentPrefab);
						mNextGrowthCounter = 0;
						mNextGrowthNeed = Mathf.RoundToInt(mNextGrowthNeed*1.35f);
					}
					
					SpawnSkull();
				}
				Heal(growth.mHealAmount);
				completedGrowths.Add(growth);
			}
		}

		foreach(var growth in completedGrowths)
		{
			mSegmentGrowths.Remove(growth);
		}
	}

	protected void UpdateSegmentGrowthSizes()
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

	public void SpawnSkull()
	{
		LinkedListNode<SerpentSegment> tail = mSegments.Last;
		Vector3 up = Random.onUnitSphere;
		up.Normalize();
		Quaternion rot = Quaternion.LookRotation(Vector3.forward, up);
		GameObject skullObj = Instantiate(mSkullPrefab, tail.Value.transform.position, rot) as GameObject;
		Destroy(skullObj, 10.0f);
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

		if(mReentryInputBlockerTimer > 0 || mHealth <= 0)
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

				float bonusSpeed = Mathf.Max(0, mSegments.Count - mNumInitialSegments)*mSpeedBonusPerSegment;

				Vector3 desiredVel = finalDir * distFromDesiredPos * (mSpeedMultiplier + bonusSpeed);// * Time.deltaTime;
				Vector3.SmoothDamp(vel, desiredVel, ref vel, 1.0f, mMaxSpeed, Time.deltaTime);

				mRigidbody.velocity = vel;
			}
		}

		UpdateLooking();
	}

	protected float mZUndulationTimer = 0;
	protected void UpdateLooking()
	{
		mZUndulationTimer += Time.deltaTime;
		if(mRigidbody.velocity.sqrMagnitude > 0)
		{
			Vector3 zUndulationFactor = Vector3.forward*Mathf.Sin(Mathf.PI*4.0f*mZUndulationTimer)*0.25f;
			Vector3 rotateLook = Vector3.back*2;// * (1.0f-Mathf.Clamp01(mRigidbody.velocity.magnitude));
			transform.LookAt(mRigidbody.position+mRigidbody.velocity + zUndulationFactor + rotateLook, transform.position.normalized);
			//Debug.Log(rotateLook);

		}
	}
	
	public void MoveToward(Vector3 worldPos)
	{
		mDesiredPos = worldPos;
	}

	public void Attack(Vector3 worldPos)
	{
		Vector3 desiredDir = worldPos - transform.position;
		desiredDir.Normalize();
		mRigidbody.AddForce(desiredDir*mAttackForce, ForceMode.Impulse);
	}

	public void TakeDamage(int damageAmount = 1)
	{
		mHealth -= damageAmount;
		AudioController.Instance.PlayHurtSfx ();
		if(mHealth <= 0)
		{
			mHealth = 0;
			// Death
			AudioController.Instance.ToGameOverSnapshot(3);
		}
	}

	public void Heal(int healAmount = 1)
	{
		if(mHealth <= 0)
		{
			return;
		}

		mHealth = Mathf.Min(mHealth + healAmount, MaxHealth);
	}

	public override void OnCollisionEnter(Collision c)
	{
		IEatable eatable = c.collider.GetComponentInParent(typeof(IEatable)) as IEatable;
		if(eatable != null)
		{
			mAnimator.SetTrigger("Bite");
			eatable.BeEaten(this);
		}
        else
        {
            base.OnCollisionEnter(c);
        }
	}
}
