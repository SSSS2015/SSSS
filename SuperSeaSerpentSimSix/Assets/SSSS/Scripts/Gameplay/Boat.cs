using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

	public Buoyancy mBuoyancy;

	public float mCapsizeTime = 2.0f;
	protected float mCapsizeTimer = 0.0f;

	public GameObject[] mPeoplePrefabs;
	public Transform[] mSpawnPositions;

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

		int numSlots = mSpawnPositions.Length;
		int forceSpawnIndex = Random.Range(0, numSlots); // ensure that at least one person is on this boat

		for(int i = 0; i < numSlots; ++i)
		{
			if(i != forceSpawnIndex && Random.value > 0.6f)
			{
				continue;
			}

			int leftOrRight = (Random.value > 0.5f)?1:-1;
			float range = mBuoyancy.mEndsOffset;
			Vector3 personPos = mSpawnPositions[i].position;
			GameObject prefab = mPeoplePrefabs[Random.Range(0, numPeopleOptions)];
			sector.SpawnEntity(prefab, personPos, transform.up, Vector3.forward*leftOrRight); 
		}
	}
}
