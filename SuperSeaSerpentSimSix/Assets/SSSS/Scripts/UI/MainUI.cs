using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour {
	public Text mHealthText;
	protected int mDisplayHealth = -1;

	public Text mScoreText;
	protected int mScoreDisplay = -1;

	public Text mNexSegmentText;
	protected int mDisplayNextSegment = -1;

	protected World mWorld;

	public void Start()
	{
		mWorld = World.Instance;
	}

	public void Update()
	{
		if(mWorld.mSerpent.Health != mDisplayHealth)
		{
			mDisplayHealth = mWorld.mSerpent.Health;
			mHealthText.text = string.Format("{0}/{1}", mDisplayHealth, mWorld.mSerpent.MaxHealth);
		}
		if(mWorld.mScoreManager.Score != mScoreDisplay)
		{
			mScoreDisplay = mWorld.mScoreManager.Score;
			mScoreText.text = string.Format("{0}", mScoreDisplay);
		}
		if(mWorld.mSerpent.NextGrowthCounter != mDisplayNextSegment)
		{
			mDisplayNextSegment = mWorld.mSerpent.NextGrowthCounter;
			mNexSegmentText.text = string.Format("{0}/{1}", mWorld.mSerpent.NextGrowthCounter, mWorld.mSerpent.NextGrowthNeed);
		}
	}
}
