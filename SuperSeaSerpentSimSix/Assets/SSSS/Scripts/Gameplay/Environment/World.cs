using UnityEngine;
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

    private WaveManager mWaveManager;

	public void Awake()
	{
		if(sInstance != null)
		{
			Destroy(this);
			return;
		}

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

	public Vector2 GetPolarCoordinate(Vector3 worldPos)
	{
		Vector2 flattenedPos = new Vector2(worldPos.x, worldPos.y);
		return new Vector2(flattenedPos.magnitude, Mathf.Atan2(flattenedPos.x, flattenedPos.y));
	}

	public Vector3 GetWorldCoordinate(Vector2 polarPos)
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
