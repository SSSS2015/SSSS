using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

	public Buoyancy mBuoyancy;

	public int mMinPeople = 1;
	public int mMaxPeople = 3;

	public float mCapsizeTime = 2.0f;
	protected float mCapsizeTimer = 0.0f;

	public GameObject[] mPeoplePrefabs;

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

	public void SpawnPeople(Sector sector)
	{
		int numPeopleOptions = mPeoplePrefabs.Length;
		if(numPeopleOptions <= 0)
		{
			return;
		}

		int numPeople = Random.Range(mMinPeople, mMaxPeople+1);
		for(int p = 0; p < numPeople; ++p)
		{
			int leftOrRight = (Random.value > 0.5f)?1:-1;
			float range = mBuoyancy.mEndsOffset;
			Vector3 personPos = transform.position + transform.right*Random.Range(-range,range) + transform.up*0.5f;
			GameObject prefab = mPeoplePrefabs[Random.Range(0, numPeopleOptions)];
			sector.SpawnEntity(prefab, personPos, transform.up, Vector3.forward*leftOrRight); 
		}
	}
}
