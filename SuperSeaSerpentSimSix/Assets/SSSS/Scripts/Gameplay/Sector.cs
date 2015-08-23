using UnityEngine;
using System.Collections.Generic;

public class Sector
{
	protected World mWorld;
	protected int mIndex;
	public int Index { get { return mIndex; } }
	
	protected float mStart;
	public float Start { get { return mStart; } }
	protected float mEnd;
	public float End { get { return mEnd; } }
	
	protected List<GameObject> mEntities = new List<GameObject>();
	public List<GameObject> Entities { get { return mEntities; } } 

	public bool IsActive { get { return mEntities.Count > 0; } }

	public Sector(World world, int index)
	{
		mWorld = world;
		mIndex = index;
		
		mStart = mWorld.SectorSize*index - Mathf.PI;
		mEnd = mStart + mWorld.SectorSize;
	}
	
	public void Generate()
	{
		int numFish = Random.Range(5,10);
		for(int i = 0; i < numFish; ++i)
		{
			Vector2 fishPolarPos = new Vector2(Random.Range(mWorld.SeaBedLevel, mWorld.SeaLevel-2.0f), Random.Range(mStart, mEnd));
			SpawnEntity(mWorld.mFishPrefab, fishPolarPos);
		}

		int numBoats = Random.Range(1,5);
		for(int i = 0; i < numBoats; ++i)
		{
			Vector2 boatPolarPos = new Vector2(mWorld.SeaLevel-0.5f, Random.Range(mStart, mEnd));
			GameObject boatObj = SpawnEntity(mWorld.mBoatPrefab, boatPolarPos);
			Boat boat = boatObj.GetComponent<Boat>();
			if(boat != null)
			{
				int numPeople = Random.Range(boat.mMinPeople, boat.mMaxPeople+1);
				for(int p = 0; p < numPeople; ++p)
				{
					int leftOrRight = (Random.value > 0.5f)?1:-1;
					float range = boat.mBuoyancy.mEndsOffset;
					Vector3 personPos = boat.transform.position + boat.transform.right*Random.Range(-range,range) + boat.transform.up*0.5f;
					SpawnEntity(mWorld.mPersonPrefab, personPos, boat.transform.up, Vector3.forward*leftOrRight); 
				}
			}
		}
	}

	public GameObject SpawnEntity(GameObject prefab, Vector2 polarPos)
	{
		Vector3 worldPos = mWorld.GetWorldCoordinate(polarPos);
		Vector3 up = worldPos.normalized;
		Quaternion rot = Quaternion.LookRotation(Vector3.forward*((Random.value > 0.5f)?1:-1), up);
		GameObject obj = GameObject.Instantiate(prefab, worldPos, rot) as GameObject;
		mEntities.Add(obj);
		return obj;
	}

	public GameObject SpawnEntity(GameObject prefab, Vector3 worldPos, Vector3 up, Vector3 forward)
	{
		Quaternion rot = Quaternion.LookRotation(forward, up);
		GameObject obj = GameObject.Instantiate(prefab, worldPos, rot) as GameObject;
		mEntities.Add(obj);
		return obj;
	}
	
	public void Destroy()
	{
		for(int i = 0, n = mEntities.Count; i < n; ++i)
		{
			GameObject.Destroy(mEntities[i]);
		}
		mEntities.Clear();
	}
}
