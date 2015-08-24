using UnityEngine;
using System.Collections;

public class Fish : BaseSeaCreature, IEatable {
	private static int sCachedSerpentMask = 0;
	public static int kSerpentMask { get {
			if(sCachedSerpentMask == 0)
			{
				sCachedSerpentMask = LayerMask.GetMask("Serpent");
			}
			return sCachedSerpentMask;
		}
	}

	public int mHealAmount = 1;
	public int mScoreValue = 10;

	public float mDetectionRadius = 10.0f;
	public float mFleeSpeed = 3.0f;

	public void BeEaten(Serpent eater)
	{
		// heal the serpent
		eater.Digest(mHealAmount);
		World.Instance.mScoreManager.AddScore(mScoreValue);
		AudioController.Instance.PlayPickupFishSfx ();
		Destroy(gameObject);
	}

	public void Start()
	{
		Vector3 initialVel = Random.insideUnitCircle;
		mRigidbody.velocity = initialVel;
	}

	public void Update()
	{
		if(!IsAboveWater())
		{
			int numSegmentsInRange = 0;
			Vector3 segmentsCenter = Vector3.zero;
			Collider[] colliders = Physics.OverlapSphere(transform.position, mDetectionRadius, kSerpentMask);
			for(int i = 0, n = colliders.Length; i < n; ++i)
			{
				Collider c = colliders[i];
				SerpentSegment segment = c.GetComponentInParent<SerpentSegment>();
				if(segment != null)
				{
					segmentsCenter += segment.transform.position;
					numSegmentsInRange++;
				}
			}
			
			if(numSegmentsInRange > 0)
			{
				segmentsCenter /= numSegmentsInRange;
				segmentsCenter.z = 0;
				Vector3 desiredVel = transform.position - segmentsCenter;
				desiredVel.Normalize();
				//mRigidbody.AddForce(dirAwayFromSegments * mFleeSpeed, ForceMode.VelocityChange);
				Vector3 vel = mRigidbody.velocity;
				Vector3.SmoothDamp(vel, desiredVel*mFleeSpeed, ref vel, 1.0f, mFleeSpeed, Time.deltaTime);
				
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
			Vector3 zUndulationFactor = Vector3.forward*Mathf.Sin(Mathf.PI*4.0f*mZUndulationTimer)*0.01f;
			Vector3 rotateLook = Vector3.back*0.1f;// * (1.0f-Mathf.Clamp01(mRigidbody.velocity.magnitude));
			transform.LookAt(mRigidbody.position+mRigidbody.velocity, transform.position.normalized);
		}
	}
}
