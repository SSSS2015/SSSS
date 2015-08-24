using UnityEngine;
using System.Collections;

[System.Serializable]
public class DifficultyStage {
	public int mMinSeaCreaturesToSpawn = 5;
	public int mMaxSeaCreaturesToSpawn = 10;

	public int mMinBoatToSpawn = 5;
	public int mMaxBoatToSpawn = 10;

	public GameObject[] mSeaCreaturePrefabs;
	public GameObject[] mBoatPrefabs;
	
	public GameObject GetRandomPrefab(GameObject[] prefabs)
	{
		int numPrefabs = prefabs.Length;
		if(numPrefabs <= 0)
		{
			return null;
		}

		return prefabs[Random.Range(0, numPrefabs)];
	}
}
