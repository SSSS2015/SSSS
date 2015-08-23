using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour {
	public Text mHealthText;
	protected int mDisplayHealth = 0;

	protected Serpent mSerpent;

	public void Start()
	{
		mSerpent = World.Instance.mSerpent;
	}

	public void Update()
	{
		if(mSerpent.Health != mDisplayHealth)
		{
			mDisplayHealth = mSerpent.Health;
			mHealthText.text = string.Format("Health: {0}/{1}", mDisplayHealth, mSerpent.MaxHealth);
		}
	}
}
