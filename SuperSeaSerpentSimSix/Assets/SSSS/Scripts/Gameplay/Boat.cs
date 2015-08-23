using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

	public Buoyancy mBuoyancy;

	public int mMinPeople = 1;
	public int mMaxPeople = 3;

	public float mCapsizeTime = 2.0f;
	protected float mCapsizeTimer = 0.0f;

	public void Awake()
	{
		if(mBuoyancy == null)
		{
			mBuoyancy = GetComponent<Buoyancy>();
		}
	}

	public void Update()
	{
		Vector3 down = transform.position.normalized*-1;

		if(Vector3.Dot(down, transform.up) > 0)
		{
			mCapsizeTimer += Time.deltaTime;
			if(mCapsizeTimer >= mCapsizeTime)
			{
				mBuoyancy.mBuoyancyEnabled = false;
			}
		}
		else
		{
			mCapsizeTimer = 0.0f;
		}
	}
}
