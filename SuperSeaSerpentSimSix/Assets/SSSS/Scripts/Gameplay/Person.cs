using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour, IEatable {
	private static int sCachedSerpentMask = 0;
	public static int kSerpentMask { get {
			if(sCachedSerpentMask == 0)
			{
				sCachedSerpentMask = LayerMask.GetMask("Serpent");
			}
			return sCachedSerpentMask;
		}
	}

	public float mAttackRange = 20.0f;

	public float mTellOffset = 0.3f;
	public float mAttackTellTime = 1.0f;
	protected float mAttackTellTimer = 0.0f;

	public float mMinTimeBetweenAttacks = 3.0f;
	public float mMaxTimeBetweenAttacks = 6.0f;
	protected float mAttackTimer = 0.0f;
	
	public GameObject mProjectilePrefab;
	public Transform mHand;
	protected Vector3 mOriginalHandPos;
	protected BaseProjectile mCurrentProjectile;

	public Buoyancy mBuoyancy;

	protected bool mSerpentDetected = false;
	protected Vector3 mLastKnownSerpentPos = Vector3.zero;

	protected bool mIsDrowning = false;

	public const float mDrownTime = 1.0f;
	protected float mDrownTimer = 0.0f;

	public void Awake()
	{
		if(mBuoyancy == null)
		{
			mBuoyancy = GetComponent<Buoyancy>();
		}

		mOriginalHandPos = mHand.localPosition;
	}

	public void Update()
	{
		if(mIsDrowning || mProjectilePrefab == null)
		{
			return;
		}

		bool isAboveWater = mBuoyancy.IsAboveWater();

		if(!isAboveWater)
		{
			mDrownTimer += Time.deltaTime;
			if(mDrownTimer >= mDrownTime)
			{
				mIsDrowning = true;
				DropProjectile();
				return;
			}
		}

		mDrownTimer = 0.0f;
		
		Serpent serpent = World.Instance.mSerpent;
		if(isAboveWater 
		   && Vector3.SqrMagnitude(serpent.transform.position - transform.position) < mAttackRange*mAttackRange 
		   && serpent.IsAboveWater())
		{
			mSerpentDetected = true;
			mLastKnownSerpentPos = serpent.transform.position;
		}

		if(mSerpentDetected)
		{
			if(mCurrentProjectile != null)
			{

				mAttackTellTimer -= Time.deltaTime;
				if(mAttackTellTimer <= 0)
				{
					mAttackTellTimer = 0;
					FireProjectileAt(mLastKnownSerpentPos);
					mSerpentDetected = false;
				}
				else
				{
					Vector3 tellOffsetDir = mHand.position-mLastKnownSerpentPos;
					tellOffsetDir.Normalize();
					mHand.InverseTransformDirection(tellOffsetDir);
					mHand.localPosition = mOriginalHandPos+tellOffsetDir*mTellOffset*Mathf.Clamp01(1.0f - mAttackTellTimer/mAttackTellTime);
					mHand.LookAt(mLastKnownSerpentPos);
				}
			}
			else
			{
				mAttackTimer -= Time.deltaTime;
				if(mAttackTimer <= 0)
				{
					// attack
					ReadyProjectile();
					mAttackTimer = Random.Range(mMinTimeBetweenAttacks, mMaxTimeBetweenAttacks);
				}
			}
		}
		else
		{
			mAttackTimer = Random.Range(mMinTimeBetweenAttacks, mMaxTimeBetweenAttacks);
		}
	}

	public void ReadyProjectile()
	{
		//Debug.Log("Readying Projectile");
		mHand.localPosition = mOriginalHandPos;
		GameObject obj = Instantiate(mProjectilePrefab, mHand.position, mHand.rotation) as GameObject;
		mCurrentProjectile = obj.GetComponent<BaseProjectile>();
		mCurrentProjectile.transform.parent = mHand;

		mAttackTellTimer = mAttackTellTime;
	}

	public void FireProjectileAt(Vector3 pos)
	{
		//Debug.Log("Firing Projectile");
		mCurrentProjectile.LaunchToward(pos);
		mCurrentProjectile = null;
	}

	public void DropProjectile()
	{
		if(mCurrentProjectile != null)
		{
			mCurrentProjectile.Drop();
		}
		mCurrentProjectile = null;
	}

	public void BeEaten(Serpent eater)
	{
		eater.GrowSegment();
		World.Instance.mScoreManager.AddScore(100);
		Destroy(gameObject);
	}
}
