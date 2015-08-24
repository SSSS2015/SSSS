using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public void PlayAgain()
	{
		AudioController.Instance.PlayMenuConfirmSfx ();
		AudioController.Instance.ToTitleSnapshot ();
        Application.LoadLevel(Application.loadedLevel);
    }
}
