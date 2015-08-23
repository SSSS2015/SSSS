using UnityEngine;
using System.Collections;

public class ScoreManager {
	protected int mScore = 0;
	public int Score { get { return mScore; } }

	public void AddScore(int amount)
	{
		mScore += amount;
	}
}
