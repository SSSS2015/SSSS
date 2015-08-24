using UnityEngine;
using System.Collections;

public class Fish : BaseSeaCreature, IEatable {
	public int mHealAmount = 1;

	public void BeEaten(Serpent eater)
	{
		// heal the serpent
		eater.Digest(mHealAmount);
		World.Instance.mScoreManager.AddScore(10);
		Destroy(gameObject);
	}
}
