﻿using UnityEngine;
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
	
	public void Generate(DifficultyStage data)
	{
		int numFish = Random.Range(data.mMinSeaCreaturesToSpawn,data.mMaxSeaCreaturesToSpawn);
		for(int i = 0; i < numFish; ++i)
		{
			GameObject fishPrefab = data.GetRandomPrefab(data.mSeaCreaturePrefabs);
			if(fishPrefab == null)
			{
				continue;
			}

			float theta = Random.Range(mStart, mEnd);
			Vector2 fishPolarPos = new Vector2(Random.Range(mWorld.SeaBedLevel, mWorld.GetSeaLevel(theta)-2.0f), theta);
			SpawnEntity(fishPrefab, fishPolarPos);
		}

		int numBoats = Random.Range(data.mMinBoatToSpawn,data.mMaxBoatToSpawn);
		for(int i = 0; i < numBoats; ++i)
		{
			GameObject boatPrefab = data.GetRandomPrefab(data.mBoatPrefabs);
			if(boatPrefab == null)
			{
				continue;
			}

			float theta = Random.Range(mStart, mEnd);
			Vector2 boatPolarPos = new Vector2(mWorld.GetSeaLevel(theta), theta);
			GameObject boatObj = SpawnEntity(boatPrefab, boatPolarPos);
			Boat boat = boatObj.GetComponent<Boat>();
			if(boat != null)
			{
				boat.SpawnPeople(this);
			}
		}
	}

	public GameObject SpawnEntity(GameObject prefab, Vector2 polarPos)
	{
		Vector3 worldPos = World.GetWorldCoordinate(polarPos);
		Vector3 up = worldPos.normalized;
		Quaternion rot = Quaternion.LookRotation(Vector3.forward*((Random.value > 0.5f)?1:-1), up);
		GameObject obj = GameObject.Instantiate(prefab, worldPos, rot) as GameObject;
		obj.transform.parent = mWorld.transform;
		mEntities.Add(obj);
		return obj;
	}

	public GameObject SpawnEntity(GameObject prefab, Vector3 worldPos, Vector3 up, Vector3 forward)
	{
		Quaternion rot = Quaternion.LookRotation(forward, up);
		GameObject obj = GameObject.Instantiate(prefab, worldPos, rot) as GameObject;
		obj.transform.parent = mWorld.transform;
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
