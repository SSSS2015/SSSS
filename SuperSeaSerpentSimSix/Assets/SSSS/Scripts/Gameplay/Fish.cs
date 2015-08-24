using UnityEngine;
using System.Collections;

public class Fish : BaseSeaCreature, IEatable {
	public void BeEaten(Serpent eater)
	{
		// heal the serpent
		eater.Digest();
		World.Instance.mScoreManager.AddScore(10);
		AudioController.Instance.PlayPickupFishSfx ();
		Destroy(gameObject);
	}
}
