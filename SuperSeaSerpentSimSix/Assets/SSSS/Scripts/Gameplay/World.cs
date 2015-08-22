using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	private static World sInstance;
	public static World Instance { get { return sInstance; } }

	public GameObject mSeaBackground;

	public float SeaLevel { get { return mSeaBackground.transform.localScale.x*0.5f; } }

	public void Awake()
	{
		if(sInstance != null)
		{
			Destroy(this);
			return;
		}

		sInstance = this;
	}
}
