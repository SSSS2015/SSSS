﻿using UnityEngine;
using System.Collections.Generic;

public class World : MonoBehaviour {

	private static World sInstance;
	public static World Instance { get { return sInstance; } }

	public Serpent mSerpent;

	public GameObject mSeaBackground;
	public GameObject mSeaBed;

	public float SeaLevel { get { return mSeaBackground.transform.localScale.x*0.5f; } }
	public float SeaBedLevel { get { return mSeaBed.transform.localScale.x * 0.5f; } }
		
	public int mNumSectors = 8;
	protected float mSectorSize;
	public float SectorSize { get { return mSectorSize; } }
	protected Sector[] mSectors;
	protected int mCurrentSector;

	public DifficultyStage[] mDifficultyStages;
	public int mSegmentsPerDifficultyLevel = 3;

	//public GameObject mFishPrefab;
	//public GameObject mBoatPrefab;
	public GameObject mSplashPrefab;

	public float mMinSplashSpeed = 1.0f;

	public ScoreManager mScoreManager = new ScoreManager();

    public GameObject[] mIslands;
    public float mIslandZ = 11;
    public int mNumIslandsPerSector = 2;

    public GameObject[] mClouds;
    public Vector3 mCloudPosMin;
    public Vector3 mCloudPosMax;
    public int mNumCloudsPerSector = 2;

    private WaveManager mWaveManager;

	public void Awake()
	{
		sInstance = this;

		mSectorSize = (Mathf.PI*2.0f)/mNumSectors;
		mSectors = new Sector[mNumSectors];
		for(int i = 0; i < mNumSectors; ++i)
		{
			mSectors[i] = new Sector(this, i);
		}

        mWaveManager = FindObjectOfType<WaveManager>();
	}

	public void Start()
	{
		DifficultyStage difficulty = mDifficultyStages[GetDifficultyLevel()];
			
		mCurrentSector = GetSectorIndex(mSerpent.transform.position);
		mSectors[GetLeftSectorIndex(mCurrentSector)].Generate(difficulty);
		mSectors[mCurrentSector].Generate(difficulty);
		mSectors[GetRightSectorIndex(mCurrentSector)].Generate(difficulty);

        if(mIslands.Length > 0)
        {
            for (int i = 0; i < mNumSectors; ++i)
            {
                float thetaStart = mSectorSize * i;
                float thetaEnd = mSectorSize * (i + 1);
                for(int island = 0; island < mNumIslandsPerSector; ++island)
                {
                    SpawnIsland( Random.Range(thetaStart, thetaEnd) );
                }
            }
        }

        if(mClouds.Length > 0)
        {
            for(int i = 0; i < mNumSectors; ++i)
            {
                float thetaStart = mSectorSize * i;
                float thetaEnd = mSectorSize * (i + 1);
                for(int cloud = 0; cloud < mNumCloudsPerSector; ++cloud)
                {
                    SpawnCloud(Random.Range(thetaStart, thetaEnd));
                }
            }
        }
	}

    private void SpawnIsland(float theta)
    {
        int islandIndex = Random.Range(0, mIslands.Length - 1);

        GameObject island = GameObject.Instantiate(mIslands[islandIndex]);
        Vector3 islandPosition = World.GetWorldCoordinate(new Vector2(SeaLevel, theta));

        island.transform.position = new Vector3(islandPosition.x, islandPosition.y, mIslandZ);
        island.transform.localRotation = Quaternion.LookRotation(Vector3.forward, islandPosition.normalized);

        // declutter the scene
        island.transform.parent = transform;
    }

    private void SpawnCloud(float theta)
    {
        float x = Random.Range(mCloudPosMin.x, mCloudPosMax.x);
        float y = Random.Range(mCloudPosMin.y, mCloudPosMax.y);
        float z = Random.Range(mCloudPosMin.z, mCloudPosMax.z);
        float scale = x; // Temp

        int cloudIndex = Random.Range(0, mClouds.Length - 1);
        GameObject cloud = GameObject.Instantiate(mClouds[cloudIndex]);
        Vector3 cloudPosition = World.GetWorldCoordinate(new Vector2(SeaLevel + y, theta));

        cloud.transform.position = new Vector3(cloudPosition.x, cloudPosition.y, z);
        cloud.transform.localRotation = Quaternion.LookRotation(Vector3.forward, cloudPosition.normalized);

        // declutter the scene
        cloud.transform.parent = transform;
    }

	public void Update()
	{
		//Vector2 serpentPolarPos = GetPolarCoordinate(mSerpent.transform.position);
		//Debug.Log(serpentPolarPos);
		int sector = GetSectorIndex(mSerpent.transform.position);
		if(sector != mCurrentSector)
		{
			int left = GetLeftSectorIndex(mCurrentSector);
			int right = GetRightSectorIndex(mCurrentSector);

			DifficultyStage difficulty = mDifficultyStages[GetDifficultyLevel()];

			if(sector == left)
			{
				// we moved left
				mSectors[right].Destroy();
				mSectors[GetLeftSectorIndex(sector)].Generate(difficulty);
			}
			else
			{
				// we moved right
				mSectors[left].Destroy();
				mSectors[GetRightSectorIndex(sector)].Generate(difficulty);
			}
		}

		mCurrentSector = sector;
	}

	public int GetDifficultyLevel()
	{
		int newSegments = mSerpent.Segments.Count - mSerpent.mNumInitialSegments;
        if (newSegments < 0)
            newSegments = 0;

		return Mathf.Min(newSegments/mSegmentsPerDifficultyLevel, mDifficultyStages.Length-1);
	}

	public int GetSectorIndex(Vector3 worldPos)
	{
		Vector2 polarPos = GetPolarCoordinate(worldPos);
		return Mathf.FloorToInt((polarPos.y + Mathf.PI)/mSectorSize);
	}

	public int GetLeftSectorIndex(int index)
	{
		return (index + mNumSectors - 1)%mNumSectors;
	}

	public int GetRightSectorIndex(int index)
	{
		return (index + 1)%mNumSectors;
	}

	public Sector GetSector(Vector3 worldPos)
	{
		return mSectors[GetSectorIndex(worldPos)];
	}

	public static Vector2 GetPolarCoordinate(Vector3 worldPos)
	{
		Vector2 flattenedPos = new Vector2(worldPos.x, worldPos.y);
		return new Vector2(flattenedPos.magnitude, Mathf.Atan2(flattenedPos.x, flattenedPos.y));
	}

	public static Vector3 GetWorldCoordinate(Vector2 polarPos)
	{
		return new Vector3(polarPos.x * Mathf.Sin(polarPos.y), polarPos.x * Mathf.Cos(polarPos.y), 0);
	}

    public float GetSeaLevel(float thetaRadians)
    {
        if(mWaveManager != null)
        {
            return mWaveManager.GetSeaLevel(thetaRadians);
        }
        else
        {
            return SeaLevel;
        }
    }

	public void SpawnSplash(float speed, Vector3 worldPos)
	{
		if(speed > mMinSplashSpeed)
		{
			AudioController.Instance.PlaySplashSfx();
			GameObject splashObj = Instantiate(mSplashPrefab, worldPos, Quaternion.LookRotation(worldPos.normalized, Vector3.forward)) as GameObject;
			splashObj.transform.localScale *= speed/6.0f;
			Destroy(splashObj, 1.0f);
		}
	}
}
