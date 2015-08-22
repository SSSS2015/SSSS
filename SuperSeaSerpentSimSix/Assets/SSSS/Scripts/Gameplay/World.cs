using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	private static World sInstance;
	public static World Instance { get { return sInstance; } }

	public GameObject mSerpent;

	public GameObject mSeaBackground;
	public GameObject mSeaBed;

	public float SeaLevel { get { return mSeaBackground.transform.localScale.x*0.5f; } }
	public float SeaBedLevel { get { return mSeaBed.transform.localScale.x * 0.5f; } }

	public GameObject mFishPrefab;

	public void Awake()
	{
		if(sInstance != null)
		{
			Destroy(this);
			return;
		}

		sInstance = this;

		Vector2 serpentPolarPos = GetPolarCoordinate(mSerpent.transform.position);
		float spawnRange = Mathf.PI*0.25f;
		for(int i = 0; i < 20; ++i)
		{
			Vector2 fishPolarPos = new Vector2(Random.Range(SeaBedLevel, SeaLevel), serpentPolarPos.y + Random.Range(-spawnRange, spawnRange));
			Instantiate(mFishPrefab, GetWorldCoordinate(fishPolarPos), Quaternion.identity);
		}
	}

	public Vector2 GetPolarCoordinate(Vector3 worldPos)
	{
		Vector2 flattenedPos = new Vector2(worldPos.x, worldPos.y);
		return new Vector2(flattenedPos.magnitude, Mathf.Atan2(flattenedPos.x, flattenedPos.y)+Mathf.PI*0.5f);
	}

	public Vector3 GetWorldCoordinate(Vector2 polarPos)
	{
		return new Vector3(polarPos.x * Mathf.Cos(polarPos.y), polarPos.x * Mathf.Sin(polarPos.y));
	}
}
