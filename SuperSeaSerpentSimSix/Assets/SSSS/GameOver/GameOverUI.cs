using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public void PlayAgain()
	{
		AudioController.Instance.PlayMenuConfirmSfx ();
        Application.LoadLevel(Application.loadedLevel);
    }
}
